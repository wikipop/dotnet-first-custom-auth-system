using DziegielAdminPlatform.Data;
using DziegielAdminPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace DziegielAdminPlatform.Services;

public class UserService(ApplicationDbContext context, IPasswordStore passwordStore) : IUserService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IPasswordStore _passwordStore = passwordStore;

    public async Task RegisterUserAsync(PlatformUser user, string password)
    {
        await _passwordStore.UpdateHashPasswordAsync(user, password);
        await _context.PlatformUsers.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<PasswordVerificationResult> ValidateUserAsync(string userName, string password)
    {
        var user = await _context.PlatformUsers.FirstOrDefaultAsync(u => u.UserName == userName);
        if (user == null) return PasswordVerificationResult.Failed;

        return _passwordStore.VerifyHashedPassword(user, user.PasswordHash, password);
    }
    
    public async Task<PlatformUser?> GetUserAsync(string userName)
    {
        return await _context.PlatformUsers.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<IEnumerable<PlatformUser>> GetUsersWithRolesAsync()
    {
        return await _context.PlatformUsers.Include(u => u.Roles).ToListAsync();
    }
    
    public async Task<PlatformUser?> GetUserAsync(Guid userId)
    {
        return await _context.PlatformUsers.FirstOrDefaultAsync(u => u.Id == userId);
    }
}