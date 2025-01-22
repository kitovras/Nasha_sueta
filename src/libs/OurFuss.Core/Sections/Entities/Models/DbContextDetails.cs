using Microsoft.EntityFrameworkCore;

namespace OurFuss.Core.Sections.Entities.Models;

/// <summary>
/// Information about the database context
/// </summary>
/// <typeparam name="T">DbSet</typeparam>
public class DbContextDetails<T>
{
    /// <summary>
    /// ID of the context in the database
    /// </summary>
    public DbContextId DbContextId { get; set; }

    /// <summary>
    /// Destroy the context
    /// </summary>
    public Action<DbContextId> ContextDestroy { get; set; }

    /// <summary>
    /// Request
    /// </summary>
    public IQueryable<T> Queryable { get; set; }
}
