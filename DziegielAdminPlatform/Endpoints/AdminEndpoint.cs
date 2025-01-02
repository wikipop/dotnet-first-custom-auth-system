using DziegielAdminPlatform.Data;
using DziegielAdminPlatform.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DziegielAdminPlatform.Endpoints;

public static class AdminEndpoint
{
    public static void MapAdminEndpoint(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/admin").RequireAuthorization("Admin");
        
        group.MapGet("/roles", GetRoles);
        
    }

    private static IResult GetRoles([FromServices] IServiceProvider sp)
    {
        var roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();
        var roles = roleManager.Roles.ToList();
        return Results.Ok(roles);
    }
    
    [AttributeUsage(AttributeTargets.Parameter)]
    private sealed class FromServicesAttribute : Attribute, IFromServiceMetadata {}
}