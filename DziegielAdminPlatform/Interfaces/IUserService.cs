using DziegielAdminPlatform.Models;

namespace DziegielAdminPlatform.Services;

public interface IUserService
{
    public Task RegisterUserAsync(PlatformUser user, string password);

    public Task<PasswordVerificationResult> ValidateUserAsync(string userName, string password);
    
    public Task<IEnumerable<PlatformUser>> GetUsersWithRolesAsync();
    
    public Task<PlatformUser?> GetUserAsync(string userName);

    public Task<PlatformUser?> GetUserAsync(Guid userId);
}