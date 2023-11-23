using Chess.Domain.Identity.DTO;
using MediatR;

namespace Chess.Application.Identity.Commands.SignIn;

public sealed record SignInCommand(
    string Email,
    string Password) : IRequest<JsonWebToken>;
