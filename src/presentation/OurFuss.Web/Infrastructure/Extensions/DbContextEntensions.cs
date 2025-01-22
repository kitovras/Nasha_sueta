using Microsoft.EntityFrameworkCore;
using OurFuss.Data.Postgre.Db;

namespace OurFuss.Web.Infrastructure.Extensions;

public static class DbContextEntensions
{
    public static void AddDbContextPostgre(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("ConnectionStrings:PostgreDb").Value
            ?? throw new NullReferenceException("DbConnection postgreDb is null or empty");

        services.AddDbContextFactory<OurFussContextPostgre>(option =>
        {
            option.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(OurFussContextPostgre).Assembly.FullName));
        });
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var _context = scope.ServiceProvider.GetRequiredService<OurFussContextPostgre>();
        if (_context.Database.GetPendingMigrations().Any())
        {
            _context.Database.Migrate();
        }
    }
}
