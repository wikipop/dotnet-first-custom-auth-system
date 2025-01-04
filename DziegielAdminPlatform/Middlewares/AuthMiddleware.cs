using DziegielAdminPlatform.Endpoints;
using DziegielAdminPlatform.Services;

namespace DziegielAdminPlatform.Middlewares;

public static class AuthMiddlewareBuilderExtensions
{
    public static IApplicationBuilder UseAuth(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);
        
        if(app.Environment.IsDevelopment())
        {
            return app.MapWhen(context => !context.Request.Path.StartsWithSegments(AuthEndpoint.BasePath) &&
                                         !context.Request.Path.StartsWithSegments("/scalar") &&
                                         !context.Request.Path.StartsWithSegments("/openapi"),
                appBuilder => appBuilder.UseMiddleware<AuthMiddleware>());
        }
        
        return app.MapWhen(context => !context.Request.Path.StartsWithSegments(AuthEndpoint.BasePath),
            appBuilder => appBuilder.UseMiddleware<AuthMiddleware>());
    }
}

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    
    public AuthMiddleware(RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(next);
        
        _next = next;
    }
    
    public async Task Invoke(HttpContext context, ISessionManager sessionManager)
    {
        if (context.Request.Cookies.TryGetValue("session", out var sessionId))
        {
            var session = await sessionManager.GetSessionAsync(Guid.Parse(sessionId));
            
            if (session is not null)
            {
                context.Items["session"] = session;
            }

            await _next(context);
        }
        
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
    }
}