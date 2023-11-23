using System.Security.Claims;
using Chess.Domain.Identity.DTO;

namespace Chess.Domain.Identity.Services;

public interface ITokenService
{
    Task<JsonWebToken> GenerateJwtToken(Guid userId, string email, ICollection<string> roles, ICollection<Claim> claims);
}