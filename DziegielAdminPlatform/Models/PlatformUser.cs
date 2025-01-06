namespace DziegielAdminPlatform.Models;

public class PlatformUser : PlatformUser<Guid>
{
    public PlatformUser()
    {
        Id = Guid.NewGuid();
    }

    public PlatformUser(string userName) : this()
    {
        UserName = userName;
    }
}

public class PlatformUser<TKey> where TKey : IEquatable<TKey>
{
    public PlatformUser()
    {
    }

    public PlatformUser(string userName) : this()
    {
        UserName = userName;
    }

    public virtual TKey Id { get; set; }
    public virtual string UserName { get; set; }
    public virtual string PasswordHash { get; set; }
    public virtual ICollection<PlatformRole> Roles { get; set; } = new List<PlatformRole>();
    
    public virtual bool? LockoutEnabled { get; set; }
    
    public virtual int? AccessFailedCount { get; set; }
}