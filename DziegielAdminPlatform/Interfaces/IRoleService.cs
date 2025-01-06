using DziegielAdminPlatform.Models;

namespace DziegielAdminPlatform.Services;

public interface IRoleService
{
    public Task CreateRoleAsync(PlatformRole role);
    public Task<PlatformRole?> GetRoleAsync(string roleName);
    public Task<List<PlatformRole>> GetRolesAsync();
    public Task AddUserToRoleAsync(PlatformUser user, PlatformRole role);
    public Task<bool> RoleExistsAsync(string roleName);
    public Task<bool> UserHasRoleAsync(PlatformUser user, PlatformRole role);
}