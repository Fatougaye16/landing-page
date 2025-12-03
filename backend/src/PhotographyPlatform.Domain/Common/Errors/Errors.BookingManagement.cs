using ErrorOr;
using PhotographyPlatform.Domain.BookingManagement.ValueObjects;
using PhotographyPlatform.Domain.StudioManagement.ValueObjects;

namespace PhotographyPlatform.Domain.Common.Errors;

public static partial class Errors
{
    public static class BookingManagement
    {
        public static Error BookingDateInPast => Error.Validation(
            code: "BookingManagement.BookingDateInPast",
            description: "Booking date cannot be in the past.");

        public static Error BookingNotAvailableOnSundays => Error.Validation(
            code: "BookingManagement.BookingNotAvailableOnSundays",
            description: "Bookings are not available on Sundays.");

        public static Error InvalidBookingStatus => Error.Validation(
            code: "BookingManagement.InvalidBookingStatus",
            description: "Booking status is not valid.");

        public static Error InvalidPackageType => Error.Validation(
            code: "BookingManagement.InvalidPackageType",
            description: "Package type is not valid.");

        public static Error TimeSlotNotAvailable => Error.Conflict(
            code: "BookingManagement.TimeSlotNotAvailable",
            description: "The selected time slot is not available.");

        public static Error InvalidBookingTimes => Error.Validation(
            code: "BookingManagement.InvalidBookingTimes",
            description: "Start time must be before end time.");

        public static Error BookingTooLong => Error.Validation(
            code: "BookingManagement.BookingTooLong",
            description: "Booking duration cannot exceed 8 hours.");

        public static Error BookingOutsideBusinessHours => Error.Validation(
            code: "BookingManagement.BookingOutsideBusinessHours",
            description: "Bookings must be between 8 AM and 8 PM.");
    }

    public static class Booking
    {
        public static Error NotFound(BookingId bookingId) => Error.NotFound(
            code: "Booking.NotFound",
            description: $"Booking with ID '{bookingId}' was not found.");

        public static Error TimeSlotConflict(StudioProfileId photographerId, BookingDate bookingDate, TimeOnly startTime) => Error.Conflict(
            code: "Booking.TimeSlotConflict",
            description: $"Photographer {photographerId} has a conflicting booking on {bookingDate} at {startTime}.");
    }
}