using OurFuss.Web.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
OptionExtensions.AddOptions(builder);
FileLoggerExtensions.AddLogger(builder.Environment);

builder.Services.AddDbContextPostgre(builder.Configuration);
builder.Services.ServiceCollection(builder);
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 2024;
    options.UseCaseSensitivePaths = true;
});

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.MapControllers();
app.ApplyMigrations();

await app.Services.InitAsync();
app.Run();