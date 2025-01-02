using Microsoft.AspNetCore.Identity;

namespace DziegielAdminPlatform.Models;

public class PlatformRole : IdentityRole
{
    public PlatformRole(string name) : base(name) {}
} 