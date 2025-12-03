using Microsoft.EntityFrameworkCore;
using PhotographyPlatform.Domain.Common.Abstractions;
using PhotographyPlatform.Infrastructure.Data;

namespace PhotographyPlatform.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity, TId> 
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    protected readonly PhotographyPlatformDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(PhotographyPlatformDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    protected IQueryable<TEntity> Query => _dbSet;

    protected async Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(e => e.Id.Equals(id), cancellationToken);
    }
}