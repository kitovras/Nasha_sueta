using Microsoft.EntityFrameworkCore;
using OurFuss.Core.Db;

namespace OurFuss.Extensions.Configurations;

public static class DbContextConfigurationExtension
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ArenaDbDefaultConnection");
        services.AddDbContextFactory<GlobalContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }
}
