using Chess.Domain.Identity.DTO;

namespace Chess.Domain.Identity.Services;

public interface IIdentityService
{
    Task SignUp(string email, string password, CancellationToken cancellationToken);
    Task<JsonWebToken> SignIn(string email, string password, CancellationToken cancellationToken);
}