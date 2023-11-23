using Chess.Domain.Identity.DTO;
using Chess.Domain.Identity.Services;
using MediatR;

namespace Chess.Application.Identity.Commands.SignUp;

public sealed class SignUpHandler : IRequestHandler<SignUpCommand, JsonWebToken>
{
    private readonly IIdentityService _identityService;

    public SignUpHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<JsonWebToken> Handle (SignUpCommand request, CancellationToken cancellationToken)
    {
        await _identityService.SignUp(request.Email, request.Password, cancellationToken);

        var token = await _identityService.SignIn(request.Email, request.Password, cancellationToken);

        return token;
    }
}