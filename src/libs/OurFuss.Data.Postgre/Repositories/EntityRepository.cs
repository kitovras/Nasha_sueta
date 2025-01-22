using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OurFuss.Core.Sections.Entities;
using OurFuss.Core.Sections.Entities.Models;
using OurFuss.Data.Postgre.Db;

namespace OurFuss.Data.Postgre.Repositories;

/// <inheritdoc/>
public class EntityRepository : IEntityRepository
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    private readonly OurFussContextPostgre _dbContext;

    /// <summary>
    /// Фабрика контекста базы данных
    /// </summary>
    private readonly IDbContextFactory<OurFussContextPostgre> _dbContextFactory;

    /// <summary>
    /// Лист дб контекстов в памяти
    /// </summary>
    private static BlockingCollection<OurFussContextPostgre> _dbContextMemoryList = new();

    public EntityRepository(
        OurFussContextPostgre dbContext,
        IDbContextFactory<OurFussContextPostgre> dbContextFactory)
    {
        _dbContext = dbContext;
        _dbContextFactory = dbContextFactory;
    }

    /// <inheritdoc/>
    public async Task<IDbContextTransaction> RunTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

    /// <inheritdoc/>
    public DbContextDetails<T> GetContextDetails<T>() where T : class
    {
        //Используется фабрика, для асинхронного получения данных в разных тасках
        var dbContext = _dbContextFactory.CreateDbContext();
        _dbContextMemoryList.TryAdd(dbContext, 5000);

        var dbContextDetails = new DbContextDetails<T>
        {
            DbContextId = dbContext.ContextId,
            Queryable = dbContext.Set<T>().AsNoTracking(),
            ContextDestroy = async (DbContextId) => await ContextDestroy(DbContextId)
        };

        return dbContextDetails;
    }

    /// <inheritdoc/>
    public IQueryable<T> GetQueryable<T>() where T : class
    {
        return _dbContext.Set<T>().AsNoTracking();
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

    #region Helpers

    /// <summary>
    /// Асинхронно уничтожить контекст
    /// </summary>
    /// <param name="dbContextId">Идентификатор Db-контекста</param>
    /// <returns>Асинхронная задача</returns>
    private static async Task ContextDestroy(DbContextId dbContextId)
    {
        //Без лока есть узкий момент, когда в коллекции остаются старые объекты.
        lock (_dbContextMemoryList)
        {
            var dbContext = _dbContextMemoryList.FirstOrDefault(fod => fod?.ContextId == dbContextId);
            if (dbContext is null)
                return;

            dbContext.Dispose();

            var dbContextCurrents = _dbContextMemoryList.Where(w => w.ContextId != dbContextId).ToList();
            _dbContextMemoryList = new BlockingCollection<OurFussContextPostgre>(new ConcurrentQueue<OurFussContextPostgre>(dbContextCurrents));
        }
    }

    #endregion
}