using DziegielAdminPlatform.Middlewares;
using DziegielAdminPlatform.Models;
using DziegielAdminPlatform.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace DziegielAdminPlatform.Endpoints;

public static class AdminEndpoint
{
    public static void MapAdminEndpoint(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/admin").AddEndpointFilter(new UserAccessFilter("Admin"));

        group.MapGet("/roles", GetRoles);
        group.MapGet("/users", GetUsers);
    }

    private static async Task<Ok<List<PlatformRole>>> GetRoles(IRoleService roleService)
    {
        return TypedResults.Ok(await roleService.GetRolesAsync());
    }
    
    private static async Task<Ok<IEnumerable<PlatformUser>>> GetUsers(IUserService userService)
    {
        return TypedResults.Ok(await userService.GetUsersWithRolesAsync());
    }
}