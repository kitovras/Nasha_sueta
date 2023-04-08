using Microsoft.EntityFrameworkCore;
using OurFuss.Core.Db;

namespace OurFuss.Api.Infrastructure.Extensions;

public static class DbContextConfigurationExtension
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("ConnectionStrings:OurFussDbDefaultConnection").Value;
        services.AddDbContextFactory<OurFussDbContext>(option =>
        {
            option.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(OurFussDbContext).Assembly.FullName));
        });
    }
}
