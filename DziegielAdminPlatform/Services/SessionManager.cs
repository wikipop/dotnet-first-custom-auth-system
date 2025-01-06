using DziegielAdminPlatform.Data;
using DziegielAdminPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace DziegielAdminPlatform.Services;

public class SessionManager(ApplicationDbContext context) : ISessionManager
{
    private readonly ApplicationDbContext _context = context;

    public async Task<PlatformSession?> GetSessionAsync(Guid providedSessionId)
    {
        return await ValidateSessionAsync(
            await _context.PlatformSessions.FirstOrDefaultAsync(s => s.SessionId == providedSessionId));
    }

    public async Task<PlatformSession?> GetUserSessionAsync(PlatformUser user)
    {
        return await ValidateSessionAsync(
            await _context.PlatformSessions.FirstOrDefaultAsync(s => s.UserId == user.Id));
    }

    public async Task<PlatformSession> CreateSessionAsync(PlatformUser user)
    {
        var session = new PlatformSession
        {
            SessionId = Guid.NewGuid(),
            LastAccessedAt = DateTime.UtcNow,
            UserId = user.Id
        };

        await _context.PlatformSessions.AddAsync(session);
        await _context.SaveChangesAsync();

        return session;
    }

    public Task DeleteSessionAsync(PlatformSession session)
    {
        _context.PlatformSessions.Remove(session);
        return _context.SaveChangesAsync();
    }

    public async Task<PlatformSession?> ValidateSessionAsync(PlatformSession? providedSession)
    {
        if (providedSession == null) return null;

        if (providedSession.IsExpired)
        {
            await DeleteSessionAsync(providedSession);
            return null;
        }

        return await _context.PlatformSessions.FirstOrDefaultAsync(s => s.SessionId == providedSession.SessionId);
    }
}