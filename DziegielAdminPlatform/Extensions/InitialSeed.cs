using DziegielAdminPlatform.Models;
using Microsoft.AspNetCore.Identity;

namespace DziegielAdminPlatform.Data;

public static class InitialSeed
{
    public static void SeedRoles(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<PlatformRole>>();
            var roles = new[] { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                {
                    roleManager.CreateAsync(new PlatformRole(role)).Wait();
                }
            }
        }
    }

    public static void SeedUsers(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<PlatformUser>>();
            var user = new PlatformUser { UserName = "wikipopxyz@gmail.com", Email = "wikipopxyz@gmail.com" };

            if (userManager.FindByNameAsync(user.UserName).Result == null)
            {
                Console.WriteLine("Enter password for admin user:");
                
                string password = null;
                while (true)
                {
                    var key = System.Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    password += key.KeyChar;
                }
                
                userManager.CreateAsync(user, password!).Wait();
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}