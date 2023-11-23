using Chess.Domain.Games.Entities;
using Chess.Domain.Games.Enums;

namespace Chess.Domain.Games.Repositories;

public interface IGameRepository
{
    Task<Guid> CreateAsync(Guid? whitePlayerId, Guid? blackPlayerId, ResultEnum result, int? whiteRanking, int? blackRanking, string pgn, CancellationToken cancellationToken);

}