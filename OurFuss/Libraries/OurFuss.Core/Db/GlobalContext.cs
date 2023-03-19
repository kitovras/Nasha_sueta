using Microsoft.EntityFrameworkCore;

namespace OurFuss.Core.Db;

/// <summary>
/// Глобальный контекст базы данных
/// </summary>
public class GlobalContext : DbContext
{
    public GlobalContext(DbContextOptions<GlobalContext> options) : base(options) { }
}