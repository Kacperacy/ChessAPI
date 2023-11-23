using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chess.Domain.Common.Services;
using Chess.Domain.Identity.Configurations;
using Chess.Domain.Identity.DTO;
using Chess.Domain.Identity.Services;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Chess.Infrastructure.Identity.Services;

public sealed class TokenService : ITokenService
{
    private readonly AuthConfiguration _authConfiguration;
    private readonly IDateTimeService _dateTimeService;
    private readonly SigningCredentials _signingCredentials;
    
    public TokenService(AuthConfiguration authConfiguration, IDateTimeService dateTimeService)
    {
        _authConfiguration = authConfiguration;
        _dateTimeService = dateTimeService;
        _signingCredentials = new SigningCredentials( new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.JwtKey)),
                SecurityAlgorithms.HmacSha256);
    }

    public async Task<JsonWebToken> GenerateJwtToken(Guid userId, string email, ICollection<string> roles, ICollection<Claim> claims)
    {
        var now = _dateTimeService.CurrentDate();
        var issuer = _authConfiguration.JwtIssuer;

        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
        };

        if (roles?.Any() is true)
            foreach (var role in roles)
                jwtClaims.Add(new Claim("role", role));

        if (claims?.Any() is true)
        {
            var customClaims = new List<Claim>();
            foreach (var claim in claims)
            {
                customClaims.Add(new Claim(claim.Type, claim.Value));
            }

            jwtClaims.AddRange(customClaims);
        }

        var expires = now.Add(_authConfiguration.JwtExpireTime);

        var jwt = new JwtSecurityToken(
            issuer,
            issuer,
            jwtClaims,
            now,
            expires,
            _signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JsonWebToken
        {
            AccessToken = token,
            Expires = new DateTimeOffset(expires).ToUnixTimeSeconds(),
            UserId = userId,
            Email = email,
            Roles = roles,
            Claims = claims?.ToDictionary(x => x.Type, x => x.Value)
        };
    }
}