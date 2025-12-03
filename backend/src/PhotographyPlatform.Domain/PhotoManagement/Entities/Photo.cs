using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.Common.BaseTypes;
using PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

namespace PhotographyPlatform.Domain.PhotoManagement.Entities;

public class Photo : Entity<PhotoId>
{
    public AlbumId AlbumId { get; private set; }
    public string OriginalFilePath { get; private set; }
    public string? EditedFilePath { get; private set; }
    public PhotoMetadata Metadata { get; private set; }
    public PhotoStatus Status { get; private set; }
    public bool IsSelectedForEditing { get; private set; }
    public UserId? EditedBy { get; private set; }
    public DateTime? EditedAt { get; private set; }
    public UserId? ApprovedBy { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public string? ClientNotes { get; private set; }
    public string? PhotographerNotes { get; private set; }

    private Photo(
        PhotoId id,
        AlbumId albumId,
        string originalFilePath,
        PhotoMetadata metadata) : base(id)
    {
        AlbumId = albumId;
        OriginalFilePath = originalFilePath;
        Metadata = metadata;
        Status = PhotoStatus.Unedited;
        IsSelectedForEditing = false;
    }

    public static Photo Create(
        AlbumId albumId,
        string originalFilePath,
        PhotoMetadata metadata)
    {
        if (string.IsNullOrWhiteSpace(originalFilePath))
            throw new ArgumentException("Original file path is required");

        return new Photo(
            PhotoId.CreateUnique(),
            albumId,
            originalFilePath,
            metadata);
    }

    public void SelectForEditing()
    {
        if (!Status.CanBeEditedByPhotographer())
            throw new InvalidOperationException($"Photo cannot be selected for editing in status: {Status}");

        IsSelectedForEditing = true;
        SetUpdatedAt();
    }

    public void DeselectFromEditing()
    {
        if (Status != PhotoStatus.Unedited)
            throw new InvalidOperationException("Can only deselect unedited photos");

        IsSelectedForEditing = false;
        SetUpdatedAt();
    }

    public void StartEditing(UserId photographerId)
    {
        if (!IsSelectedForEditing)
            throw new InvalidOperationException("Photo must be selected for editing first");

        if (!Status.CanBeEditedByPhotographer())
            throw new InvalidOperationException($"Photo cannot be edited in status: {Status}");

        Status = PhotoStatus.InProgress;
        EditedBy = photographerId;
        SetUpdatedAt();
    }

    public void CompleteEditing(string editedFilePath, string? photographerNotes = null)
    {
        if (Status != PhotoStatus.InProgress)
            throw new InvalidOperationException("Only photos in progress can be completed");

        if (string.IsNullOrWhiteSpace(editedFilePath))
            throw new ArgumentException("Edited file path is required");

        EditedFilePath = editedFilePath;
        Status = PhotoStatus.Edited;
        EditedAt = DateTime.UtcNow;
        PhotographerNotes = photographerNotes;
        SetUpdatedAt();
    }

    public void ApproveByClient(UserId clientId, string? clientNotes = null)
    {
        if (!Status.CanBeApprovedByClient())
            throw new InvalidOperationException($"Photo cannot be approved in status: {Status}");

        Status = PhotoStatus.ClientApproved;
        ApprovedBy = clientId;
        ApprovedAt = DateTime.UtcNow;
        ClientNotes = clientNotes;
        SetUpdatedAt();
    }

    public void RejectByClient(UserId clientId, string clientNotes)
    {
        if (Status != PhotoStatus.Edited)
            throw new InvalidOperationException("Only edited photos can be rejected");

        if (string.IsNullOrWhiteSpace(clientNotes))
            throw new ArgumentException("Client notes are required when rejecting a photo");

        Status = PhotoStatus.Rejected;
        ClientNotes = clientNotes;
        SetUpdatedAt();
    }

    public void UpdatePhotographerNotes(string? notes)
    {
        PhotographerNotes = notes;
        SetUpdatedAt();
    }

    public bool IsEdited => !string.IsNullOrEmpty(EditedFilePath);
    
    public bool IsApproved => Status == PhotoStatus.ClientApproved;
    
    public bool CanBeViewed => Status != PhotoStatus.InProgress;
}
