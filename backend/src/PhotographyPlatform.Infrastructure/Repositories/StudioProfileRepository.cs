using ErrorOr;
using Microsoft.EntityFrameworkCore;
using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.Common.Errors;
using PhotographyPlatform.Domain.StudioManagement.Entities;
using PhotographyPlatform.Domain.StudioManagement.Repositories;
using PhotographyPlatform.Domain.StudioManagement.ValueObjects;
using PhotographyPlatform.Infrastructure.Data;

namespace PhotographyPlatform.Infrastructure.Repositories;

public class StudioProfileRepository : BaseRepository<StudioProfile, StudioProfileId>, IStudioProfileRepository
{
    public StudioProfileRepository(PhotographyPlatformDbContext context) : base(context)
    {
    }

    public new async Task<ErrorOr<StudioProfile>> GetByIdAsync(StudioProfileId id, CancellationToken cancellationToken = default)
    {
        var studioProfile = await base.GetByIdAsync(id, cancellationToken);
        
        return studioProfile is null 
            ? Errors.StudioProfile.NotFound(id) 
            : studioProfile;
    }

    public async Task<ErrorOr<StudioProfile>> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        var studioProfile = await Query
            .FirstOrDefaultAsync(sp => sp.UserId == userId, cancellationToken);
        
        return studioProfile is null 
            ? Errors.StudioProfile.NotFoundByUserId(userId) 
            : studioProfile;
    }

    public async Task<bool> ExistsByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return await Query
            .AnyAsync(sp => sp.UserId == userId, cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(StudioProfileId id, CancellationToken cancellationToken = default)
    {
        return await ExistsAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyList<StudioProfile>> GetBySpecializationAsync(
        PhotographySpecialization specialization, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(sp => sp.Specialization == specialization)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<StudioProfile>> GetByLocationAsync(
        string location, 
        CancellationToken cancellationToken = default)
    {
        return await Query
            .Where(sp => sp.Location.Contains(location))
            .ToListAsync(cancellationToken);
    }

    public async Task<ErrorOr<Created>> CreateAsync(StudioProfile studioProfile, CancellationToken cancellationToken = default)
    {
        if (await ExistsByUserIdAsync(studioProfile.UserId, cancellationToken))
        {
            return Errors.StudioProfile.DuplicateForUser(studioProfile.UserId);
        }

        await AddAsync(studioProfile, cancellationToken);
        return Result.Created;
    }

    public new async Task<ErrorOr<Updated>> UpdateAsync(StudioProfile studioProfile, CancellationToken cancellationToken = default)
    {
        if (!await ExistsByIdAsync(studioProfile.Id, cancellationToken))
        {
            return Errors.StudioProfile.NotFound(studioProfile.Id);
        }

        await base.UpdateAsync(studioProfile, cancellationToken);
        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> DeleteAsync(StudioProfileId id, CancellationToken cancellationToken = default)
    {
        var studioProfile = await base.GetByIdAsync(id, cancellationToken);
        
        if (studioProfile is null)
        {
            return Errors.StudioProfile.NotFound(id);
        }

        await base.DeleteAsync(studioProfile, cancellationToken);
        return Result.Deleted;
    }
}