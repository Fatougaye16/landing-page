using ErrorOr;
using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.BookingManagement.Entities;
using PhotographyPlatform.Domain.BookingManagement.ValueObjects;
using PhotographyPlatform.Domain.StudioManagement.ValueObjects;

namespace PhotographyPlatform.Domain.BookingManagement.Repositories;

public interface IBookingRepository
{
    Task<ErrorOr<Booking>> GetByIdAsync(BookingId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Booking>> GetByClientIdAsync(UserId clientId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Booking>> GetByPhotographerIdAsync(StudioProfileId photographerId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Booking>> GetByStatusAsync(BookingStatus status, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Booking>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Booking>> GetPhotographerBookingsInDateRangeAsync(StudioProfileId photographerId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<bool> HasConflictingBookingAsync(StudioProfileId photographerId, BookingDate bookingDate, TimeOnly startTime, TimeOnly endTime, BookingId? excludeBookingId = null, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(BookingId id, CancellationToken cancellationToken = default);
    Task<ErrorOr<Created>> CreateAsync(Booking booking, CancellationToken cancellationToken = default);
    Task<ErrorOr<Updated>> UpdateAsync(Booking booking, CancellationToken cancellationToken = default);
    Task<ErrorOr<Deleted>> DeleteAsync(BookingId id, CancellationToken cancellationToken = default);
}