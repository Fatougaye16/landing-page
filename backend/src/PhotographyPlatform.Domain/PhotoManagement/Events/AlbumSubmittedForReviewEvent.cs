using PhotographyPlatform.Domain.Common.BaseTypes;
using PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

namespace PhotographyPlatform.Domain.PhotoManagement.Events;

public sealed record AlbumSubmittedForReviewEvent : DomainEvent
{
    public AlbumId AlbumId { get; init; }
    public int TotalPhotos { get; init; }
    public int SelectedPhotos { get; init; }

    public AlbumSubmittedForReviewEvent(
        AlbumId albumId,
        int totalPhotos,
        int selectedPhotos) : base()
    {
        AlbumId = albumId;
        TotalPhotos = totalPhotos;
        SelectedPhotos = selectedPhotos;
    }
}