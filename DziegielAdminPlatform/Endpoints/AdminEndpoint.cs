using DziegielAdminPlatform.Middlewares;
using Microsoft.AspNetCore.Identity;

namespace DziegielAdminPlatform.Endpoints;

public static class AdminEndpoint
{
    public static void MapAdminEndpoint(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/admin").AddEndpointFilter(new UserAccessFilter("Admin"));

        group.MapGet("/roles", GetRoles);
    }

    private static IResult GetRoles()
    {
        return Results.Ok("Roles");
    }
}