using Microsoft.EntityFrameworkCore;

namespace OurFuss.Core.Db;

/// <summary>
/// Контекст базы данных
/// </summary>
public class OurFussDbContext : DbContext
{
    public OurFussDbContext(DbContextOptions<OurFussDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EventConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    #region DbSets

    /// <summary>
    /// Таблица событий
    /// </summary>
    internal DbSet<EventEntity> Events => Set<EventEntity>();

    #endregion
}