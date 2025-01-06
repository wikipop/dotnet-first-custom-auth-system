using System.Security.Cryptography;
using DziegielAdminPlatform.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace DziegielAdminPlatform.Services;

public class PasswordStore : IPasswordStore
{
    private readonly int _iterCount = 10000;
    private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

    public async Task UpdateHashPasswordAsync(PlatformUser user, string password)
    {
        var salt = new byte[128 / 8];
        _rng.GetBytes(salt);

        user.PasswordHash = HashPassword(password, salt);

        await Task.CompletedTask;
    }

    public PasswordVerificationResult VerifyHashedPassword(PlatformUser user, string hashedPassword,
        string providedPassword)
    {
        var salt = Convert.FromBase64String(hashedPassword.Split('.')[1]);

        var providedPasswordHash = HashPassword(providedPassword, salt);

        return providedPasswordHash == user.PasswordHash
            ? PasswordVerificationResult.Success
            : PasswordVerificationResult.Failed;
    }

    private string HashPassword(string password, byte[] salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            _iterCount,
            256 / 8)) + "." + Convert.ToBase64String(salt);
    }
}