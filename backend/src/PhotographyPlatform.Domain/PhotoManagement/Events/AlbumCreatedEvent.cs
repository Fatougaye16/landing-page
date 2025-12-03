using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.Common.BaseTypes;
using PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

namespace PhotographyPlatform.Domain.PhotoManagement.Events;

public sealed record AlbumCreatedEvent : DomainEvent
{
    public AlbumId AlbumId { get; init; }
    public UserId ClientId { get; init; }
    public UserId PhotographerId { get; init; }
    public string Title { get; init; }
    public DateTime EventDate { get; init; }

    public AlbumCreatedEvent(
        AlbumId albumId,
        UserId clientId,
        UserId photographerId,
        string title,
        DateTime eventDate) : base()
    {
        AlbumId = albumId;
        ClientId = clientId;
        PhotographerId = photographerId;
        Title = title;
        EventDate = eventDate;
    }
}