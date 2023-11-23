using Chess.Application.Games.DTO;
using Chess.Domain.Games.Entities;

namespace Chess.Infrastructure.Games.Queries;

internal static class Extensions
{
    public static GameDto AsDto(this Game game)
    {
        return new GameDto
        {
            Id = game.Id,
            Result = game.Result,
            WhitePlayer = game.WhitePlayer,
            BlackPlayer = game.BlackPlayer,
            WhiteRanking = game.WhiteRanking,
            BlackRanking = game.BlackRanking,
        };
    }
}