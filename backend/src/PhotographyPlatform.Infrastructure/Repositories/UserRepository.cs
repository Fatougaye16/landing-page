using ErrorOr;
using Microsoft.EntityFrameworkCore;
using PhotographyPlatform.Domain.Authentication.Entities;
using PhotographyPlatform.Domain.Authentication.Repositories;
using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.Common.Errors;
using PhotographyPlatform.Infrastructure.Data;

namespace PhotographyPlatform.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User, UserId>, IUserRepository
{
    public UserRepository(PhotographyPlatformDbContext context) : base(context)
    {
    }

    public new async Task<ErrorOr<User>> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        var user = await base.GetByIdAsync(id, cancellationToken);
        
        return user is null 
            ? Errors.User.NotFound(id) 
            : user;
    }

    public async Task<ErrorOr<User>> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        var user = await Query
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        
        return user is null 
            ? Errors.User.NotFoundByEmail(email) 
            : user;
    }

    public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await Query
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return await ExistsAsync(id, cancellationToken);
    }

    public async Task<ErrorOr<Created>> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        if (await ExistsByEmailAsync(user.Email, cancellationToken))
        {
            return Errors.User.DuplicateEmail(user.Email);
        }

        await AddAsync(user, cancellationToken);
        return Result.Created;
    }

    public new async Task<ErrorOr<Updated>> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        if (!await ExistsByIdAsync(user.Id, cancellationToken))
        {
            return Errors.User.NotFound(user.Id);
        }

        await base.UpdateAsync(user, cancellationToken);
        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> DeleteAsync(UserId id, CancellationToken cancellationToken = default)
    {
        var user = await base.GetByIdAsync(id, cancellationToken);
        
        if (user is null)
        {
            return Errors.User.NotFound(id);
        }

        await base.DeleteAsync(user, cancellationToken);
        return Result.Deleted;
    }
}