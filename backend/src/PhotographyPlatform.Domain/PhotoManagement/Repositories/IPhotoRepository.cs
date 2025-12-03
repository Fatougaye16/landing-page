using ErrorOr;
using PhotographyPlatform.Domain.PhotoManagement.Entities;
using PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

namespace PhotographyPlatform.Domain.PhotoManagement.Repositories;

public interface IPhotoRepository
{
    Task<ErrorOr<Photo>> GetByIdAsync(PhotoId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Photo>> GetByAlbumIdAsync(AlbumId albumId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Photo>> GetByStatusAsync(PhotoStatus status, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Photo>> GetSelectedPhotosAsync(AlbumId albumId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Photo>> GetEditedPhotosAsync(AlbumId albumId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Photo>> GetUnprocessedPhotosAsync(CancellationToken cancellationToken = default);
    Task<int> GetPhotoCountByAlbumAsync(AlbumId albumId, CancellationToken cancellationToken = default);
    Task<int> GetSelectedPhotoCountByAlbumAsync(AlbumId albumId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(PhotoId id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByFileNameAsync(AlbumId albumId, string fileName, CancellationToken cancellationToken = default);
    Task<ErrorOr<Created>> CreateAsync(Photo photo, CancellationToken cancellationToken = default);
    Task<ErrorOr<Created>> CreateRangeAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken = default);
    Task<ErrorOr<Updated>> UpdateAsync(Photo photo, CancellationToken cancellationToken = default);
    Task<ErrorOr<Updated>> UpdateRangeAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken = default);
    Task<ErrorOr<Deleted>> DeleteAsync(PhotoId id, CancellationToken cancellationToken = default);
}