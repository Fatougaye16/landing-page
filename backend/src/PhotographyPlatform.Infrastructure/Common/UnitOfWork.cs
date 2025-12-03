using ErrorOr;
using PhotographyPlatform.Domain.Common.Errors;
using PhotographyPlatform.Infrastructure.Data;

namespace PhotographyPlatform.Infrastructure.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly PhotographyPlatformDbContext _context;

    public UnitOfWork(PhotographyPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Success>> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }
        catch (Exception ex)
        {
            return Errors.Database.SaveChanges(ex.Message);
        }
    }

    public async Task<ErrorOr<TResult>> ExecuteInTransactionAsync<TResult>(
        Func<CancellationToken, Task<ErrorOr<TResult>>> operation,
        CancellationToken cancellationToken = default)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        
        try
        {
            var result = await operation(cancellationToken);
            
            if (result.IsError)
            {
                await transaction.RollbackAsync(cancellationToken);
                return result;
            }

            var saveResult = await SaveChangesAsync(cancellationToken);
            if (saveResult.IsError)
            {
                await transaction.RollbackAsync(cancellationToken);
                return saveResult.Errors;
            }

            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Errors.Database.Transaction(ex.Message);
        }
    }
}