using Microsoft.EntityFrameworkCore.Storage;
using OurFuss.Core.Db;

namespace OurFuss.Core.Modules.Common.Repositories;

/// <inheritdoc/>
public class EntityRepository : IEntityRepository
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    private readonly OurFussDbContext _dbContext;

    public EntityRepository(OurFussDbContext wheatDbContext)
    {
        _dbContext = wheatDbContext;
    }

    /// <inheritdoc/>
    public async Task<IDbContextTransaction> RunTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

    /// <inheritdoc/>
    public async Task AddAsync<T>(T entity)
    {
        if (entity is null)
            return;

        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task AddRangeAsync<T>(List<T> entities)
    {
        if (!entities.Any())
            return;
        
        await _dbContext.AddRangeAsync(entities.Cast<object>());
        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task RemoveAsync<T>(T entity)
    {
        if (entity is null)
            return;

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task RemoveRangeAsync<T>(List<T> entities)
    {
        if (!entities.Any())
            return;

        _dbContext.RemoveRange(entities.Cast<object>());
        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateAsync<T>(T entity)
    {
        if (entity is null)
            return;

        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateRangeAsync<T>(List<T> entities)
    {
        if (!entities.Any())
            return;

        _dbContext.UpdateRange(entities.Cast<object>());
        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public IQueryable<T> GetQueryable<T>() where T : class
    {
        return _dbContext.Set<T>().AsQueryable();
    }
}