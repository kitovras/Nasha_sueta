using Serilog;
using Serilog.Events;

namespace OurFuss.Web.Infrastructure.Extensions;

public static class FileLoggerExtensions
{
    public static void AddLogger(IWebHostEnvironment webHostEnvironment)
    {
        if (webHostEnvironment.IsEnvironment("TestIntegration"))
            return;

        var filePath = $"log_.log";// webHostEnvironment.IsDevelopment()
            //? $"/logs/log_.log"
            //: ;

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
            .MinimumLevel.Override("HealthChecks", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .WriteTo.Async(a => a.File(
                filePath,
                rollingInterval: RollingInterval.Day, //The interval of writing to the file
                fileSizeLimitBytes: 524288000, //mb size max
                retainedFileCountLimit: 10, //count file log max
                rollOnFileSizeLimit: true,
                shared: true,
                flushToDiskInterval: TimeSpan.FromSeconds(1)))
            .CreateLogger();
    }
}