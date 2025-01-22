using Microsoft.EntityFrameworkCore.Storage;
using OurFuss.Core.Sections.Entities.Models;

namespace OurFuss.Core.Sections.Entities;

/// <summary>
/// Репозиторий сущностей
/// </summary>
public interface IEntityRepository
{
    /// <summary>
    /// Асинхронно добавить
    /// </summary>
    /// <typeparam name="T">Абстрактный тип</typeparam>
    /// <param name="entity">Абстрактная сущность</param>
    /// <returns>Асинхронная задача</returns>
    Task AddAsync<T>(T entity);

    /// <summary>
    /// Асинхронно добавить коллекцию
    /// </summary>
    /// <typeparam name="T">Абстрактный тип</typeparam>
    /// <param name="entities">Абстрактная коллекция</param>
    /// <returns>Асинхронная задача</returns>
    Task AddRangeAsync<T>(List<T> entities);

    /// <summary>
    /// Асинхронно удалить
    /// </summary>
    /// <typeparam name="T">Абстрактный тип</typeparam>
    /// <param name="entity">Абстрактная сущность</param>
    /// <returns>Асинхронная задача</returns>
    Task RemoveAsync<T>(T entity);

    /// <summary>
    /// Асинхронно удалить коллекцию
    /// </summary>
    /// <typeparam name="T">Абстрактный тип</typeparam>
    /// <param name="entities">Абстрактная коллекция</param>
    /// <returns>Асинхронная задача</returns>
    Task RemoveRangeAsync<T>(List<T> entities);

    /// <summary>
    /// Асинхронно обновить
    /// </summary>
    /// <typeparam name="T">Абстрактный тип</typeparam>
    /// <param name="entity">Абстрактная сущность</param>
    /// <returns>Асинхронная задача</returns>
    Task UpdateAsync<T>(T entity);

    /// <summary>
    /// Асинхронно обновить коллекцию
    /// </summary>
    /// <typeparam name="T">Абстрактный тип</typeparam>
    /// <param name="entities">Абстрактная коллекция</param>
    /// <returns>Асинхронная задача</returns>
    Task UpdateRangeAsync<T>(List<T> entities);

    /// <summary>
    /// Асинхронный запуск транзакции
    /// </summary>
    /// <returns>Транзакция базы данных</returns>
    Task<IDbContextTransaction> RunTransactionAsync();

    /// <summary>
    /// Получить детали контекста
    /// </summary>
    /// <typeparam name="T">Абстрактный тип</typeparam>
    /// <returns>Детали контекста</returns>
    DbContextDetails<T> GetContextDetails<T>() where T : class;

    /// <summary>
    /// Получить запрос
    /// </summary>
    /// <typeparam name="T">Dbset сущности</typeparam>
    /// <returns>Запрос в статичном контексте</returns>
    IQueryable<T> GetQueryable<T>() where T : class;
}
