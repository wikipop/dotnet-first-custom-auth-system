using DziegielAdminPlatform.Data;
using DziegielAdminPlatform.Models;

namespace DziegielAdminPlatform.Extensions;

public static class PolicyExtension
{
    public static void AddAuthServices(this IServiceCollection service)
    {
        service.AddIdentityApiEndpoints<PlatformUser>()
            .AddRoles<PlatformRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        service.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            options.AddPolicy("User", policy => policy.RequireRole("User"));
        });
    }
}