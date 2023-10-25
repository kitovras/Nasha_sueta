using Microsoft.OpenApi.Models;
using OurFuss.Api.Infrastructure.Extensions;

namespace OurFuss.Api.Infrastructure.Extensions;

public static class SwaggerGenExtensions
{
    public static void AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
         {
             c.SwaggerDoc("v1",
                 new OpenApiInfo
                 {
                     Title = "Wheat API - V1",
                     Version = "v1"
                 }
              );

             var filePath = Path.Combine(AppContext.BaseDirectory, "OurFuss.Api.xml");
             c.IncludeXmlComments(filePath);
         });
    }
}