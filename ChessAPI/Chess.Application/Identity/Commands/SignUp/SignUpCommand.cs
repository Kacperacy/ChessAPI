using Chess.Domain.Identity.DTO;
using MediatR;

namespace Chess.Application.Identity.Commands.SignUp;

public sealed record SignUpCommand(
    string Email,
    string Password,
    string ConfirmPassword) : IRequest<JsonWebToken>;
