using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.BookingManagement.Events;
using PhotographyPlatform.Domain.BookingManagement.ValueObjects;
using PhotographyPlatform.Domain.Common.BaseTypes;
using PhotographyPlatform.Domain.StudioManagement.ValueObjects;

namespace PhotographyPlatform.Domain.BookingManagement.Entities;

public class Booking : AggregateRoot<BookingId>
{
    public UserId ClientId { get; private set; }
    public StudioProfileId PhotographerId { get; private set; }
    public BookingDate BookingDate { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public PackageType Package { get; private set; }
    public Money TotalAmount { get; private set; }
    public BookingStatus Status { get; private set; }
    public string? SpecialRequests { get; private set; }
    public DateTime? ConfirmedAt { get; private set; }
    public DateTime? CancelledAt { get; private set; }
    public string? CancellationReason { get; private set; }

    private Booking(
        BookingId id,
        UserId clientId,
        StudioProfileId photographerId,
        BookingDate date,
        TimeOnly startTime,
        TimeOnly endTime,
        PackageType package,
        Money totalAmount,
        string? specialRequests = null) : base(id)
    {
        ClientId = clientId;
        PhotographerId = photographerId;
        BookingDate = date;
        StartTime = startTime;
        EndTime = endTime;
        Package = package;
        TotalAmount = totalAmount;
        Status = BookingStatus.Pending;
        SpecialRequests = specialRequests;
    }

    public static Booking Create(
        UserId clientId,
        StudioProfileId photographerId,
        BookingDate date,
        TimeOnly startTime,
        TimeOnly endTime,
        PackageType package,
        Money totalAmount,
        string? specialRequests = null)
    {
        var booking = new Booking(
            BookingId.CreateUnique(),
            clientId,
            photographerId,
            date,
            startTime,
            endTime,
            package,
            totalAmount,
            specialRequests);

        booking.RaiseDomainEvent(new BookingCreatedEvent(booking.Id, clientId, photographerId, date.Value));

        return booking;
    }

    public void Confirm()
    {
        if (Status != BookingStatus.Pending)
            throw new InvalidOperationException("Only pending bookings can be confirmed");

        if (!BookingDate.IsAtLeast24HoursFromNow())
            throw new InvalidOperationException("Cannot confirm bookings less than 24 hours in advance");

        Status = BookingStatus.Confirmed;
        ConfirmedAt = DateTime.UtcNow;

        RaiseDomainEvent(new BookingConfirmedEvent(Id, ClientId, PhotographerId, BookingDate.Value));
    }

    public void Cancel(string reason)
    {
        if (Status == BookingStatus.Cancelled)
            throw new InvalidOperationException("Booking is already cancelled");

        if (Status == BookingStatus.Completed)
            throw new InvalidOperationException("Cannot cancel completed bookings");

        Status = BookingStatus.Cancelled;
        CancelledAt = DateTime.UtcNow;
        CancellationReason = reason;
    }

    public void Complete()
    {
        if (Status != BookingStatus.Confirmed)
            throw new InvalidOperationException("Only confirmed bookings can be completed");

        if (BookingDate.Value.Date > DateTime.UtcNow.Date)
            throw new InvalidOperationException("Cannot complete future bookings");

        Status = BookingStatus.Completed;
    }

    public void UpdateSpecialRequests(string? specialRequests)
    {
        if (Status != BookingStatus.Pending)
            throw new InvalidOperationException("Cannot update special requests for non-pending bookings");

        SpecialRequests = specialRequests;
    }

    public TimeSpan GetDuration()
    {
        return EndTime.ToTimeSpan() - StartTime.ToTimeSpan();
    }

    public bool IsUpcoming()
    {
        return BookingDate.Value.Date >= DateTime.UtcNow.Date && Status == BookingStatus.Confirmed;
    }

    public bool CanBeCancelled()
    {
        return (Status == BookingStatus.Pending || Status == BookingStatus.Confirmed) &&
               BookingDate.IsAtLeast24HoursFromNow();
    }
}