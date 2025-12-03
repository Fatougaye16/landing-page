using ErrorOr;
using Microsoft.EntityFrameworkCore;
using PhotographyPlatform.Domain.Common.Errors;
using PhotographyPlatform.Domain.PhotoManagement.Entities;
using PhotographyPlatform.Domain.PhotoManagement.Repositories;
using PhotographyPlatform.Domain.PhotoManagement.ValueObjects;
using PhotographyPlatform.Infrastructure.Data;

namespace PhotographyPlatform.Infrastructure.Repositories;

public class PhotoRepository : BaseRepository<Photo, PhotoId>, IPhotoRepository
{
    public PhotoRepository(PhotographyPlatformDbContext context) : base(context)
    {
    }

    public new async Task<ErrorOr<Photo>> GetByIdAsync(PhotoId id, CancellationToken cancellationToken = default)
    {
        var photo = await base.GetByIdAsync(id, cancellationToken);
        
        return photo is null 
            ? Errors.Photo.NotFound(id) 
            : photo;
    }

    public async Task<IReadOnlyList<Photo>> GetByAlbumIdAsync(
        AlbumId albumId, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(p => p.AlbumId == albumId)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Photo>> GetByStatusAsync(
        PhotoStatus status, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(p => p.Status == status)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Photo>> GetSelectedPhotosAsync(
        AlbumId albumId, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(p => p.AlbumId == albumId && p.IsSelectedForEditing)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Photo>> GetEditedPhotosAsync(
        AlbumId albumId, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(p => p.AlbumId == albumId && 
                       (p.Status == PhotoStatus.Edited || p.Status == PhotoStatus.ClientApproved))
            .OrderBy(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Photo>> GetUnprocessedPhotosAsync(CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(p => p.Status == PhotoStatus.Unedited)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetPhotoCountByAlbumAsync(AlbumId albumId, CancellationToken cancellationToken = default)
    {
        return await Query
            .CountAsync(p => p.AlbumId == albumId, cancellationToken);
    }

    public async Task<int> GetSelectedPhotoCountByAlbumAsync(AlbumId albumId, CancellationToken cancellationToken = default)
    {
        return await Query
            .CountAsync(p => p.AlbumId == albumId && p.IsSelectedForEditing, cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(PhotoId id, CancellationToken cancellationToken = default)
    {
        return await ExistsAsync(id, cancellationToken);
    }

    public async Task<bool> ExistsByFileNameAsync(
        AlbumId albumId, 
        string fileName, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .AnyAsync(p => p.AlbumId == albumId && p.OriginalFilePath.EndsWith(fileName), cancellationToken);
    }

    public async Task<ErrorOr<Created>> CreateAsync(Photo photo, CancellationToken cancellationToken = default)
    {
        var fileName = Path.GetFileName(photo.OriginalFilePath);
        if (await ExistsByFileNameAsync(photo.AlbumId, fileName, cancellationToken))
        {
            return Errors.Photo.DuplicateFileName(photo.AlbumId, fileName);
        }

        await AddAsync(photo, cancellationToken);
        return Result.Created;
    }

    public async Task<ErrorOr<Created>> CreateRangeAsync(
        IEnumerable<Photo> photos, 
        CancellationToken cancellationToken = default)
    {
        var photoList = photos.ToList();
        
        // Check for duplicate file names within the batch
        var duplicates = photoList
            .GroupBy(p => new { p.AlbumId, FileName = Path.GetFileName(p.OriginalFilePath) })
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicates.Any())
        {
            var firstDuplicate = duplicates.First();
            return Errors.Photo.DuplicateFileName(firstDuplicate.AlbumId, firstDuplicate.FileName);
        }

        // Check for existing file names in database
        foreach (var photo in photoList)
        {
            var fileName = Path.GetFileName(photo.OriginalFilePath);
            if (await ExistsByFileNameAsync(photo.AlbumId, fileName, cancellationToken))
            {
                return Errors.Photo.DuplicateFileName(photo.AlbumId, fileName);
            }
        }

        await AddRangeAsync(photoList, cancellationToken);
        return Result.Created;
    }

    public new async Task<ErrorOr<Updated>> UpdateAsync(Photo photo, CancellationToken cancellationToken = default)
    {
        if (!await ExistsByIdAsync(photo.Id, cancellationToken))
        {
            return Errors.Photo.NotFound(photo.Id);
        }

        await base.UpdateAsync(photo, cancellationToken);
        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateRangeAsync(
        IEnumerable<Photo> photos, 
        CancellationToken cancellationToken = default)
    {
        var photoList = photos.ToList();
        
        foreach (var photo in photoList)
        {
            if (!await ExistsByIdAsync(photo.Id, cancellationToken))
            {
                return Errors.Photo.NotFound(photo.Id);
            }
        }

        foreach (var photo in photoList)
        {
            await base.UpdateAsync(photo, cancellationToken);
        }

        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> DeleteAsync(PhotoId id, CancellationToken cancellationToken = default)
    {
        var photo = await base.GetByIdAsync(id, cancellationToken);
        
        if (photo is null)
        {
            return Errors.Photo.NotFound(id);
        }

        await base.DeleteAsync(photo, cancellationToken);
        return Result.Deleted;
    }
}