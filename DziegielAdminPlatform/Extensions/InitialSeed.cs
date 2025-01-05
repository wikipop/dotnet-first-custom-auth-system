using DziegielAdminPlatform.Models;
using DziegielAdminPlatform.Services;
using Microsoft.AspNetCore.Identity;

namespace DziegielAdminPlatform.Extensions;

public static class InitialSeedExtensions
{
    public static async Task SeedDziegielPlatform (this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        await InitialSeed.SeedRoles(scope.ServiceProvider);
        await InitialSeed.SeedUsers(scope.ServiceProvider);
    }
}

public static class InitialSeed
{
    public static async Task SeedRoles(IServiceProvider sp)
    {
        var roleManager = sp.GetRequiredService<IRoleService>();
        
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateRoleAsync(new PlatformRole()
            {
                Name = "Admin"
            });
        }
        
        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateRoleAsync(new PlatformRole()
            {
                Name = "User"
            });
        }
    }

    public static async Task SeedUsers(IServiceProvider sp)
    {
        var userManager = sp.GetRequiredService<IUserService>();
        var roleManager = sp.GetRequiredService<IRoleService>();
        var passwordStore = sp.GetRequiredService<IPasswordStore>();

        if (await userManager.GetUserAsync("admin") == null)
        {
            var user = new PlatformUser()
            {
                UserName = "admin"
            };
            
            await userManager.RegisterUserAsync(user, "0S81uUgwRKmNdGGghL40KOW7j");
            await roleManager.AddUserToRoleAsync(user, (await roleManager.GetRoleAsync("Admin"))!);
        }
    }
}