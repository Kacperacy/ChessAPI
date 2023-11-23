using Chess.Domain.Identity.DTO;
using Chess.Domain.Identity.Services;
using MediatR;

namespace Chess.Application.Identity.Commands.SignIn;

public sealed class SignInHandler : IRequestHandler<SignInCommand, JsonWebToken>
{
    private readonly IIdentityService _identityService;
    
    public SignInHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<JsonWebToken> Handle (SignInCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.SignIn(request.Email, request.Password, cancellationToken);
    }
}