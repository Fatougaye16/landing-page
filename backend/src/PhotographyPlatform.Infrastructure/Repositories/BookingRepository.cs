namespace PhotographyPlatform.Infrastructure.Repositories;

public class BookingRepository : BaseRepository<Booking, BookingId>, IBookingRepository
{
    public BookingRepository(PhotographyPlatformDbContext context) : base(context)
    {
    }

    public new async Task<ErrorOr<Booking>> GetByIdAsync(BookingId id, CancellationToken cancellationToken = default)
    {
        var booking = await base.GetByIdAsync(id, cancellationToken);
        
        return booking is null 
            ? Errors.Booking.NotFound(id) 
            : booking;
    }

    public async Task<IReadOnlyList<Booking>> GetByClientIdAsync(
        UserId clientId, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(b => b.ClientId == clientId)
            .OrderByDescending(b => b.BookingDate.Value)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Booking>> GetByPhotographerIdAsync(
        StudioProfileId photographerId, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(b => b.PhotographerId == photographerId)
            .OrderByDescending(b => b.BookingDate.Value)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Booking>> GetByStatusAsync(
        BookingStatus status, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(b => b.Status == status)
            .OrderByDescending(b => b.BookingDate.Value)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Booking>> GetByDateRangeAsync(
        DateTime startDate, 
        DateTime endDate, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(b => b.BookingDate.Value >= startDate && b.BookingDate.Value <= endDate)
            .OrderBy(b => b.BookingDate.Value)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Booking>> GetPhotographerBookingsInDateRangeAsync(
        StudioProfileId photographerId, 
        DateTime startDate, 
        DateTime endDate, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(b => b.PhotographerId == photographerId && 
                       b.BookingDate.Value >= startDate && 
                       b.BookingDate.Value <= endDate)
            .OrderBy(b => b.BookingDate.Value)
            .ThenBy(b => b.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> HasConflictingBookingAsync(
        StudioProfileId photographerId, 
        BookingDate bookingDate, 
        TimeOnly startTime, 
        TimeOnly endTime, 
        BookingId? excludeBookingId = null, 
        CancellationToken cancellationToken = default)
    {
        var query = Query
            .Where(b => b.PhotographerId == photographerId &&
                       b.BookingDate == bookingDate &&
                       b.Status != BookingStatus.Cancelled &&
                       ((b.StartTime <= startTime && b.EndTime > startTime) ||
                        (b.StartTime < endTime && b.EndTime >= endTime) ||
                        (b.StartTime >= startTime && b.EndTime <= endTime)));

        if (excludeBookingId != null)
        {
            query = query.Where(b => b.Id != excludeBookingId);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(BookingId id, CancellationToken cancellationToken = default)
    {
        return await ExistsAsync(id, cancellationToken);
    }

    public async Task<ErrorOr<Created>> CreateAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        var hasConflict = await HasConflictingBookingAsync(
            booking.PhotographerId,
            booking.BookingDate,
            booking.StartTime,
            booking.EndTime,
            cancellationToken: cancellationToken);

        if (hasConflict)
        {
            return Errors.Booking.TimeSlotConflict(booking.PhotographerId, booking.BookingDate, booking.StartTime);
        }

        await AddAsync(booking, cancellationToken);
        return Result.Created;
    }

    public new async Task<ErrorOr<Updated>> UpdateAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        if (!await ExistsByIdAsync(booking.Id, cancellationToken))
        {
            return Errors.Booking.NotFound(booking.Id);
        }

        var hasConflict = await HasConflictingBookingAsync(
            booking.PhotographerId,
            booking.BookingDate,
            booking.StartTime,
            booking.EndTime,
            booking.Id,
            cancellationToken);

        if (hasConflict)
        {
            return Errors.Booking.TimeSlotConflict(booking.PhotographerId, booking.BookingDate, booking.StartTime);
        }

        await base.UpdateAsync(booking, cancellationToken);
        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> DeleteAsync(BookingId id, CancellationToken cancellationToken = default)
    {
        var booking = await base.GetByIdAsync(id, cancellationToken);
        
        if (booking is null)
        {
            return Errors.Booking.NotFound(id);
        }

        await base.DeleteAsync(booking, cancellationToken);
        return Result.Deleted;
    }
}