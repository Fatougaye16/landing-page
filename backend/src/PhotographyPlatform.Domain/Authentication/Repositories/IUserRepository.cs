using ErrorOr;
using PhotographyPlatform.Domain.Authentication.Entities;
using PhotographyPlatform.Domain.Authentication.ValueObjects;

namespace PhotographyPlatform.Domain.Authentication.Repositories;

public interface IUserRepository
{
    Task<ErrorOr<User>> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);
    Task<ErrorOr<User>> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(UserId id, CancellationToken cancellationToken = default);
    Task<ErrorOr<Created>> CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<ErrorOr<Updated>> UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task<ErrorOr<Deleted>> DeleteAsync(UserId id, CancellationToken cancellationToken = default);
}