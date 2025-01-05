using DziegielAdminPlatform.Endpoints;
using DziegielAdminPlatform.Services;

namespace DziegielAdminPlatform.Middlewares;

public static class AuthMiddlewareBuilderExtensions
{
    public static IApplicationBuilder UseAuth(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);
        return app.UseMiddleware<AuthMiddleware>();
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
            session = await sessionManager.ValidateSessionAsync(session);
            
            if (session is not null)
            {
                context.Items["session"] = session;
            }

        }
        
        await _next(context);
    }
}