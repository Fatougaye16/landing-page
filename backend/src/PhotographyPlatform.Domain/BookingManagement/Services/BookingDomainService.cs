using ErrorOr;
using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.BookingManagement.Entities;
using PhotographyPlatform.Domain.BookingManagement.Repositories;
using PhotographyPlatform.Domain.BookingManagement.ValueObjects;
using PhotographyPlatform.Domain.Common.Errors;
using PhotographyPlatform.Domain.StudioManagement.ValueObjects;

namespace PhotographyPlatform.Domain.BookingManagement.Services;

public class BookingDomainService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingDomainService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<ErrorOr<Booking>> CreateBookingAsync(
        UserId clientId,
        StudioProfileId photographerId,
        DateTime date,
        TimeSpan startTime,
        TimeSpan endTime,
        PackageType package,
        Money totalAmount,
        string? specialRequests = null,
        CancellationToken cancellationToken = default)
    {
        // Validate booking time constraints
        var timeValidation = ValidateBookingTimes(startTime, endTime);
        if (timeValidation.IsError)
            return timeValidation.Errors;

        var bookingDateResult = BookingDate.Create(date);
        if (bookingDateResult.IsError)
            return bookingDateResult.Errors;

        var bookingDate = bookingDateResult.Value;

        // Check if the time slot is available
        var hasConflict = await _bookingRepository.HasConflictingBookingAsync(
            photographerId, 
            bookingDate, 
            TimeOnly.FromTimeSpan(startTime), 
            TimeOnly.FromTimeSpan(endTime), 
            cancellationToken: cancellationToken);

        if (hasConflict)
        {
            return Errors.Booking.TimeSlotConflict(photographerId, bookingDate, TimeOnly.FromTimeSpan(startTime));
        }

        // Create the booking
        var booking = Booking.Create(
            clientId,
            photographerId,
            bookingDate,
            TimeOnly.FromTimeSpan(startTime),
            TimeOnly.FromTimeSpan(endTime),
            package,
            totalAmount,
            specialRequests);

        return booking;
    }

    public async Task<bool> CanCancelBookingAsync(
        BookingId bookingId,
        UserId userId,
        CancellationToken cancellationToken = default)
    {
        var bookingResult = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken);
        
        if (bookingResult.IsError)
            return false;

        var booking = bookingResult.Value;

        // Only the client can cancel (photographers should use different workflow)
        if (booking.ClientId != userId)
            return false;

        return booking.CanBeCancelled();
    }

    public async Task<ErrorOr<Money>> CalculateRefundAmountAsync(
        BookingId bookingId,
        CancellationToken cancellationToken = default)
    {
        var bookingResult = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken);
        
        if (bookingResult.IsError)
            return Errors.Booking.NotFound(bookingId);

        var booking = bookingResult.Value;

        var hoursUntilBooking = booking.BookingDate.Value.Subtract(DateTime.UtcNow.Date).TotalHours;

        // Refund policy: Full refund if cancelled 48+ hours before, 50% if 24-48 hours, no refund if less than 24 hours
        ErrorOr<Money> refundResult = hoursUntilBooking switch
        {
            >= 48 => booking.TotalAmount,
            >= 24 => booking.TotalAmount.Multiply(0.5m),
            _ => Money.Create(0, booking.TotalAmount.Currency)
        };

        return refundResult;
    }

    private static ErrorOr<Success> ValidateBookingTimes(TimeSpan startTime, TimeSpan endTime)
    {
        if (startTime >= endTime)
            return Errors.BookingManagement.InvalidBookingTimes;

        if (endTime.Subtract(startTime).TotalHours > 8)
            return Errors.BookingManagement.BookingTooLong;

        if (startTime < TimeSpan.FromHours(8) || endTime > TimeSpan.FromHours(20))
            return Errors.BookingManagement.BookingOutsideBusinessHours;

        return Result.Success;
    }
}