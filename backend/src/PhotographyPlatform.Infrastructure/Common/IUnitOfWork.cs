using ErrorOr;

namespace PhotographyPlatform.Infrastructure.Common;

public interface IUnitOfWork
{
    Task<ErrorOr<Success>> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<TResult>> ExecuteInTransactionAsync<TResult>(
        Func<CancellationToken, Task<ErrorOr<TResult>>> operation,
        CancellationToken cancellationToken = default);
}