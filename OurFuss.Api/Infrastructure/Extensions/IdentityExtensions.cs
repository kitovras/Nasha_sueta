using Microsoft.AspNetCore.Identity;
using OurFuss.Core.Db;
using OurFuss.Core.Db.Entities.User;
using System.Security.Claims;
using System.Security.Principal;

namespace OurFuss.Api.Infrastructure.Extensions;

public static class IdentityExtensions
{
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<UserEntity, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 12;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            options.Lockout.AllowedForNewUsers = true;

            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;

            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.";
        }).AddEntityFrameworkStores<OurFussDbContext>().AddDefaultTokenProviders().AddUserManager<UserManager<UserEntity>>();
    }

    public static string GetUserId(this IIdentity identity)
    {
        if (identity.IsAuthenticated == false)
            throw new Exception($"Not identity");

        return ((ClaimsIdentity)identity).FindFirst(ClaimTypes.NameIdentifier)!.Value;
    }

    public static string GetUserId(this ClaimsPrincipal claims) => claims.Identity!.GetUserId();
}
