using DziegielAdminPlatform.Models;
using DziegielAdminPlatform.Services;

namespace DziegielAdminPlatform.Middlewares;

public class UserAccessFilter : IEndpointFilter
{
    private readonly string _roleName;
    
    public UserAccessFilter(string roleName)
    {
        ArgumentNullException.ThrowIfNull(roleName);
        
        _roleName = roleName;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
        var roleManager = context.HttpContext.RequestServices.GetRequiredService<IRoleService>();
        
        var session = context.HttpContext.Items["session"] as PlatformSession;
        
        if (session == null)
        {
            return TypedResults.Unauthorized();
        }
        
        var user = await userService.GetUserAsync(session.UserId);
        var role = await roleManager.GetRoleAsync(_roleName);

        if (role == null || user == null)
        {
            return TypedResults.Problem(statusCode: 501, title: "Role or user not found");
        }
        
        if (await roleManager.UserHasRoleAsync(user, role))
        {
            return await next(context);
        }
        else
        {
            return TypedResults.Forbid();
        }
    }
}