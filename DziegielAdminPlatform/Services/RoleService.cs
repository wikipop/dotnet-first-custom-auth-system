using DziegielAdminPlatform.Data;
using DziegielAdminPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace DziegielAdminPlatform.Services;

public class RoleService(ApplicationDbContext context) : IRoleService
{
    private readonly ApplicationDbContext _context = context;
    
    public async Task CreateRoleAsync(PlatformRole role)
    {
        await _context.PlatformRoles.AddAsync(role);
        await _context.SaveChangesAsync();
    }

    public async Task<PlatformRole?> GetRoleAsync(string roleName)
    {
        return await _context.PlatformRoles.FirstOrDefaultAsync(r => r.Name == roleName);
    }

    public Task AddUserToRoleAsync(PlatformUser user, PlatformRole role)
    {
        var userRole = new UserRole()
        {
            UserId = user.Id,
            RoleId = role.Id
        };
        
        _context.UsersRoles.Add(userRole);
        return _context.SaveChangesAsync();
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _context.PlatformRoles.AnyAsync(r => r.Name == roleName);
    }

    public async Task<bool> UserHasRoleAsync(PlatformUser user, PlatformRole role)
    {
        return await _context.UsersRoles.AnyAsync(ur => ur.UserId == user.Id && ur.RoleId == role.Id);
    }
}