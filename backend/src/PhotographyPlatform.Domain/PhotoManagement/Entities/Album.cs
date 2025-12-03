using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.BookingManagement.ValueObjects;
using PhotographyPlatform.Domain.Common.BaseTypes;
using PhotographyPlatform.Domain.PhotoManagement.Events;
using PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

namespace PhotographyPlatform.Domain.PhotoManagement.Entities;

public class Album : AggregateRoot<AlbumId>
{
    private readonly List<Photo> _photos = new();

    public BookingId BookingId { get; private set; }
    public UserId ClientId { get; private set; }
    public UserId PhotographerId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public AlbumStatus Status { get; private set; }
    public DateTime EventDate { get; private set; }
    public DateTime? SubmittedForReviewAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public IReadOnlyList<Photo> Photos => _photos.AsReadOnly();

    private Album(
        AlbumId id,
        BookingId bookingId,
        UserId clientId,
        UserId photographerId,
        string title,
        DateTime eventDate,
        string? description = null) : base(id)
    {
        BookingId = bookingId;
        ClientId = clientId;
        PhotographerId = photographerId;
        Title = title;
        Description = description;
        EventDate = eventDate;
        Status = AlbumStatus.Draft;
    }

    public static Album Create(
        BookingId bookingId,
        UserId clientId,
        UserId photographerId,
        string title,
        DateTime eventDate,
        string? description = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Album title is required");

        var album = new Album(
            AlbumId.CreateUnique(),
            bookingId,
            clientId,
            photographerId,
            title,
            eventDate,
            description);

        album.RaiseDomainEvent(new AlbumCreatedEvent(
            album.Id,
            clientId,
            photographerId,
            title,
            eventDate));

        return album;
    }

    public void AddPhoto(string originalFilePath, PhotoMetadata metadata)
    {
        if (!Status.CanAddPhotos())
            throw new InvalidOperationException($"Cannot add photos to album in status: {Status}");

        var photo = Photo.Create(Id, originalFilePath, metadata);
        _photos.Add(photo);
        SetUpdatedAt();
    }

    public void AddPhotos(IEnumerable<(string filePath, PhotoMetadata metadata)> photoData)
    {
        foreach (var (filePath, metadata) in photoData)
        {
            AddPhoto(filePath, metadata);
        }
    }

    public void RemovePhoto(PhotoId photoId)
    {
        if (!Status.CanAddPhotos())
            throw new InvalidOperationException($"Cannot remove photos from album in status: {Status}");

        var photo = _photos.FirstOrDefault(p => p.Id == photoId);
        if (photo != null)
        {
            _photos.Remove(photo);
            SetUpdatedAt();
        }
    }

    public void SelectPhotosForEditing(IEnumerable<PhotoId> photoIds)
    {
        var photosToSelect = _photos.Where(p => photoIds.Contains(p.Id)).ToList();
        
        if (photosToSelect.Count != photoIds.Count())
            throw new ArgumentException("Some photos were not found in the album");

        foreach (var photo in photosToSelect)
        {
            photo.SelectForEditing();
        }
        
        SetUpdatedAt();
    }

    public void SubmitForReview()
    {
        if (!Status.CanBeSubmittedForReview())
            throw new InvalidOperationException($"Album cannot be submitted for review in status: {Status}");

        if (!_photos.Any())
            throw new InvalidOperationException("Cannot submit empty album for review");

        if (!_photos.Any(p => p.IsSelectedForEditing))
            throw new InvalidOperationException("At least one photo must be selected for editing");

        Status = AlbumStatus.ReadyForReview;
        SubmittedForReviewAt = DateTime.UtcNow;
        SetUpdatedAt();

        RaiseDomainEvent(new AlbumSubmittedForReviewEvent(
            Id,
            GetTotalPhotoCount(),
            GetSelectedPhotoCount()));
    }

    public void StartReview()
    {
        if (!Status.CanBeReviewed())
            throw new InvalidOperationException($"Album cannot be reviewed in status: {Status}");

        Status = AlbumStatus.InReview;
        SetUpdatedAt();
    }

    public void CompleteAlbum()
    {
        if (Status != AlbumStatus.InReview)
            throw new InvalidOperationException("Only albums in review can be completed");

        var selectedPhotos = _photos.Where(p => p.IsSelectedForEditing);
        if (selectedPhotos.Any(p => !p.IsApproved))
            throw new InvalidOperationException("All selected photos must be approved before completing the album");

        Status = AlbumStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        SetUpdatedAt();
    }

    public void Archive()
    {
        if (!Status.IsCompleted())
            throw new InvalidOperationException("Only completed albums can be archived");

        Status = AlbumStatus.Archived;
        SetUpdatedAt();
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Album title is required");

        if (!Status.CanAddPhotos())
            throw new InvalidOperationException("Cannot update title of album not in draft status");

        Title = title;
        SetUpdatedAt();
    }

    public void UpdateDescription(string? description)
    {
        if (!Status.CanAddPhotos())
            throw new InvalidOperationException("Cannot update description of album not in draft status");

        Description = description;
        SetUpdatedAt();
    }

    // Query methods
    public IReadOnlyList<Photo> GetUneditedPhotos()
    {
        return _photos.Where(p => p.Status == PhotoStatus.Unedited).ToList().AsReadOnly();
    }

    public IReadOnlyList<Photo> GetSelectedPhotos()
    {
        return _photos.Where(p => p.IsSelectedForEditing).ToList().AsReadOnly();
    }

    public IReadOnlyList<Photo> GetEditedPhotos()
    {
        return _photos.Where(p => p.Status == PhotoStatus.Edited).ToList().AsReadOnly();
    }

    public IReadOnlyList<Photo> GetApprovedPhotos()
    {
        return _photos.Where(p => p.IsApproved).ToList().AsReadOnly();
    }

    public int GetTotalPhotoCount() => _photos.Count;
    
    public int GetSelectedPhotoCount() => _photos.Count(p => p.IsSelectedForEditing);
    
    public int GetEditedPhotoCount() => _photos.Count(p => p.IsEdited);
    
    public int GetApprovedPhotoCount() => _photos.Count(p => p.IsApproved);

    public decimal GetCompletionPercentage()
    {
        var selectedCount = GetSelectedPhotoCount();
        if (selectedCount == 0) return 0;
        
        var approvedCount = GetApprovedPhotoCount();
        return Math.Round((decimal)approvedCount / selectedCount * 100, 1);
    }
}
