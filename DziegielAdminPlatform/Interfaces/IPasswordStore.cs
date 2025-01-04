using DziegielAdminPlatform.Models;

namespace DziegielAdminPlatform.Services;

public interface IPasswordStore
{
    public Task UpdateHashPasswordAsync(PlatformUser user, string password);

    public PasswordVerificationResult VerifyHashedPassword(PlatformUser user, string hashedPassword,
        string providedPassword);
}

public enum PasswordVerificationResult
{
    /// <summary>
    ///     Indicates password verification failed.
    /// </summary>
    Failed = 0,

    /// <summary>
    ///     Indicates password verification was successful.
    /// </summary>
    Success = 1,
}