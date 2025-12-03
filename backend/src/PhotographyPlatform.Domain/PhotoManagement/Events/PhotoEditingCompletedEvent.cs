using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.Common.BaseTypes;
using PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

namespace PhotographyPlatform.Domain.PhotoManagement.Events;

public sealed record PhotoEditingCompletedEvent : DomainEvent
{
    public PhotoId PhotoId { get; init; }
    public AlbumId AlbumId { get; init; }
    public UserId PhotographerId { get; init; }
    public string EditedFilePath { get; init; }

    public PhotoEditingCompletedEvent(
        PhotoId photoId,
        AlbumId albumId,
        UserId photographerId,
        string editedFilePath) : base()
    {
        PhotoId = photoId;
        AlbumId = albumId;
        PhotographerId = photographerId;
        EditedFilePath = editedFilePath;
    }
}