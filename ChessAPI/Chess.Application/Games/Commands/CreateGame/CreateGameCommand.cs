using Chess.Domain.Games.Enums;
using MediatR;

namespace Chess.Application.Games.Commands.CreateGame;

public sealed record CreateGameCommand(
    Guid? WhitePlayerId,
    Guid? BlackPlayerId,
    ResultEnum Result,
    int? WhiteRanking,
    int? BlackRanking,
    string PGN) : IRequest<Guid>;