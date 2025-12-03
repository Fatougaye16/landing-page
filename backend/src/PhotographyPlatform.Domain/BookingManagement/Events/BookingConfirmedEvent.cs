using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.BookingManagement.ValueObjects;
using PhotographyPlatform.Domain.Common.BaseTypes;
using PhotographyPlatform.Domain.StudioManagement.ValueObjects;

namespace PhotographyPlatform.Domain.BookingManagement.Events;

public record BookingConfirmedEvent : DomainEvent
{
    public BookingId BookingId { get; init; }
    public UserId ClientId { get; init; }
    public StudioProfileId PhotographerId { get; init; }
    public DateTime BookingDate { get; init; }

    public BookingConfirmedEvent(BookingId bookingId, UserId clientId, StudioProfileId photographerId, DateTime bookingDate)
    {
        BookingId = bookingId;
        ClientId = clientId;
        PhotographerId = photographerId;
        BookingDate = bookingDate;
    }
}