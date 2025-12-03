namespace PhotographyPlatform.Infrastructure.Repositories;

public class AlbumRepository : BaseRepository<Album, AlbumId>, IAlbumRepository
{
    public AlbumRepository(PhotographyPlatformDbContext context) : base(context)
    {
    }

    public new async Task<ErrorOr<Album>> GetByIdAsync(AlbumId id, CancellationToken cancellationToken = default)
    {
        var album = await Query
            .Include(a => a.Photos)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        
        return album is null 
            ? Errors.Album.NotFound(id) 
            : album;
    }

    public async Task<ErrorOr<Album>> GetByIdWithPhotosAsync(AlbumId id, CancellationToken cancellationToken = default)
    {
        var album = await Query
            .Include(a => a.Photos)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        
        return album is null 
            ? Errors.Album.NotFound(id) 
            : album;
    }

    public async Task<IReadOnlyList<Album>> GetByBookingIdAsync(
        BookingId bookingId, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(a => a.BookingId == bookingId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Album>> GetByClientIdAsync(
        UserId clientId, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(a => a.ClientId == clientId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Album>> GetByStatusAsync(
        AlbumStatus status, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(a => a.Status == status)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Album>> GetSharedAlbumsAsync(CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(a => a.Status == AlbumStatus.Completed)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(AlbumId id, CancellationToken cancellationToken = default)
    {
        return await ExistsAsync(id, cancellationToken);
    }

    public async Task<bool> ExistsByBookingIdAsync(BookingId bookingId, CancellationToken cancellationToken = default)
    {
        return await Query
            .AnyAsync(a => a.BookingId == bookingId, cancellationToken);
    }

    public async Task<ErrorOr<Created>> CreateAsync(Album album, CancellationToken cancellationToken = default)
    {
        await AddAsync(album, cancellationToken);
        return Result.Created;
    }

    public new async Task<ErrorOr<Updated>> UpdateAsync(Album album, CancellationToken cancellationToken = default)
    {
        if (!await ExistsByIdAsync(album.Id, cancellationToken))
        {
            return Errors.Album.NotFound(album.Id);
        }

        await base.UpdateAsync(album, cancellationToken);
        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> DeleteAsync(AlbumId id, CancellationToken cancellationToken = default)
    {
        var album = await base.GetByIdAsync(id, cancellationToken);
        
        if (album is null)
        {
            return Errors.Album.NotFound(id);
        }

        await base.DeleteAsync(album, cancellationToken);
        return Result.Deleted;
    }
}