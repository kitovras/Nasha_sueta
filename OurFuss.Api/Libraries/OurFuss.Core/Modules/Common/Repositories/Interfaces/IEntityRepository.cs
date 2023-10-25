using Microsoft.EntityFrameworkCore.Storage;

namespace OurFuss.Core.Modules.Common.Repositories;

/// <summary>
/// Репозиторий сущностей
/// </summary>
/// <typeparam name="T">Сущность</typeparam>
public interface IEntityRepository
{
    /// <summary>
    /// Получить запрос
    /// </summary>
    /// <typeparam name="T">Абстрактный тип</typeparam>
    /// <returns>Запрос</returns>
    IQueryable<T> GetQueryable<T>() where T : class;

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
}