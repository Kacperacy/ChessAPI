using System.Text;
using Chess.Domain.Identity.Configurations;
using Chess.Domain.Identity.Entities;
using Chess.Infrastructure.EF.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


namespace Chess.API.Extensions;

public static class IdentityExtension
{
    public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var authConfiguration = new AuthConfiguration();
        configuration.GetSection("AuthenticationConfiguration").Bind(authConfiguration);

        services.AddSingleton(authConfiguration);

        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<EFContext>()
            .AddDefaultTokenProviders();
        
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = authConfiguration.RequireDigit;
            options.Password.RequireLowercase = authConfiguration.RequireLowercase;
            options.Password.RequireNonAlphanumeric = authConfiguration.RequireNonAlphanumeric;
            options.Password.RequireUppercase = authConfiguration.RequireUppercase;
            options.Password.RequiredLength = authConfiguration.RequiredLength;
            
            options.User.RequireUniqueEmail = authConfiguration.RequireUniqueEmail;
            options.SignIn.RequireConfirmedEmail = authConfiguration.RequireConfirmedEmail;
            
            options.Lockout.AllowedForNewUsers = authConfiguration.AllowedForNewUsers;
            options.Lockout.MaxFailedAccessAttempts = authConfiguration.MaxFailedAccessAttempts;
            options.Lockout.DefaultLockoutTimeSpan = authConfiguration.DefaultLockoutTimeSpan;
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = authConfiguration.JwtIssuer,
                ValidAudience = authConfiguration.JwtIssuer,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.JwtKey)),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.Configure<SecurityStampValidatorOptions>(options =>
        {
            options.ValidationInterval = TimeSpan.FromMinutes(1);
            options.OnRefreshingPrincipal = (context) => Task.CompletedTask;
        });
        
        return services;
    }
}