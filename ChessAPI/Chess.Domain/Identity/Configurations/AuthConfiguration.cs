namespace Chess.Domain.Identity.Configurations;

public class AuthConfiguration
{
    public string JwtKey { get; set; }
    public string JwtIssuer { get; set; }
    public TimeSpan JwtExpireTime { get; set; }
    
    // Password
    public bool RequireDigit { get; set; } = true;
    public bool RequireLowercase { get; set; } = true;
    public bool RequireNonAlphanumeric { get; set; } = true;
    public bool RequireUppercase { get; set; } = true;
    public int RequiredLength { get; set; } = 8;
    
    // Email
    public bool RequireUniqueEmail { get; set; } = true;
    public bool RequireConfirmedEmail { get; set; } = false;
    
    // Lockout
    public bool AllowedForNewUsers { get; set; } = true;
    public int MaxFailedAccessAttempts { get; set; } = 5;
    public TimeSpan DefaultLockoutTimeSpan { get; set; } = TimeSpan.FromMinutes(5);
}