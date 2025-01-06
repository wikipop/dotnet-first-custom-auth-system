using DziegielAdminPlatform.Models;

namespace DziegielAdminPlatform.Services;

public interface ISessionManager
{
    public Task<PlatformSession?> GetSessionAsync(Guid providedSessionId);

    public Task<PlatformSession?> GetUserSessionAsync(PlatformUser user);

    public Task<PlatformSession> CreateSessionAsync(PlatformUser user);

    public Task DeleteSessionAsync(PlatformSession session);

    public Task<PlatformSession?> ValidateSessionAsync(PlatformSession? session);
}