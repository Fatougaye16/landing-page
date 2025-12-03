using ErrorOr;
using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.BookingManagement.ValueObjects;
using PhotographyPlatform.Domain.PhotoManagement.Entities;
using PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

namespace PhotographyPlatform.Domain.PhotoManagement.Repositories;

public interface IAlbumRepository
{
    Task<ErrorOr<Album>> GetByIdAsync(AlbumId id, CancellationToken cancellationToken = default);
    Task<ErrorOr<Album>> GetByIdWithPhotosAsync(AlbumId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Album>> GetByBookingIdAsync(BookingId bookingId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Album>> GetByClientIdAsync(UserId clientId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Album>> GetByStatusAsync(AlbumStatus status, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Album>> GetSharedAlbumsAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(AlbumId id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByBookingIdAsync(BookingId bookingId, CancellationToken cancellationToken = default);
    Task<ErrorOr<Created>> CreateAsync(Album album, CancellationToken cancellationToken = default);
    Task<ErrorOr<Updated>> UpdateAsync(Album album, CancellationToken cancellationToken = default);
    Task<ErrorOr<Deleted>> DeleteAsync(AlbumId id, CancellationToken cancellationToken = default);
}