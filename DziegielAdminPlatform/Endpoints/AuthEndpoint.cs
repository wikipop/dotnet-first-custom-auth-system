using DziegielAdminPlatform.Models;
using DziegielAdminPlatform.Models.Requests;
using DziegielAdminPlatform.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DziegielAdminPlatform.Endpoints;

public static class AuthEndpoint
{
    public static readonly PathString BasePath = new("/auth");

    public static void MapAuthEndpoint(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BasePath);

        group.MapPost("/register", RegisterUserAsync);
        group.MapPost("/login", LoginUserAsync);
        group.MapPost("/logout", LogoutUserAsync);
    }

    private static async Task<Results<Ok, ValidationProblem>> RegisterUserAsync(
        IUserService userService,
        RegisterRequest register)
    {
        var user = new PlatformUser
        {
            UserName = register.UserName
        };

        await userService.RegisterUserAsync(user, register.Password);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, UnauthorizedHttpResult, InternalServerError>> LoginUserAsync(
        IUserService userService,
        ISessionManager sessionManager,
        HttpContext httpContext,
        LoginRequest login)
    {
        if (await userService.ValidateUserAsync(login.UserName, login.Password) == PasswordVerificationResult.Failed)
            return TypedResults.Unauthorized();

        var user = await userService.GetUserAsync(login.UserName);
        if (user is null) return TypedResults.InternalServerError();

        var session = await sessionManager.GetUserSessionAsync(user) ?? await sessionManager.CreateSessionAsync(user);

        httpContext.Response.Cookies.Append("session", session.SessionId.ToString(), new CookieOptions(){
            IsEssential = true,
            HttpOnly = true,
        });

        return TypedResults.Ok();
    }
    
    private static async Task<Results<Ok, UnauthorizedHttpResult>> LogoutUserAsync(
        ISessionManager sessionManager,
        HttpContext httpContext)
    {
        if (httpContext.Items.TryGetValue("session", out var sessionObj) && sessionObj is PlatformSession session)
        {
            await sessionManager.DeleteSessionAsync(session);
            httpContext.Response.Cookies.Delete("session");
            return TypedResults.Ok();
        }
        
        return TypedResults.Unauthorized();
    }
}