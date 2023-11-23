using Chess.Domain.Games.Enums;
using Chess.Domain.Identity.Entities;

namespace Chess.Domain.Games.Entities;

public sealed class Game : Entity
{
    public Guid? WhitePlayerId { get; private set; }
    public Guid? BlackPlayerId { get; private set; }
    public ResultEnum Result { get; private set; }
    public int? WhiteRanking { get; private set; }
    public int? BlackRanking { get; private set; }
    public string PGN { get; private set; }
    
    public User? WhitePlayer { get; private set; }
    public User? BlackPlayer { get; private set; }

    private Game() {}
    
    private Game(Guid? whitePlayerId,
        Guid? blackPlayerId,
        ResultEnum result,
        int? whiteRanking,
        int? blackRanking,
        string pgn)
    {
        WhitePlayerId = whitePlayerId;
        BlackPlayerId = blackPlayerId;
        Result = result;
        WhiteRanking = whiteRanking;
        BlackRanking = blackRanking;
        PGN = pgn;
    }
    
    public static Game Create(Guid? whitePlayerId,
        Guid? blackPlayerId,
        ResultEnum result,
        int? whiteRanking,
        int? blackRanking,
        string pgn)
        => new(whitePlayerId, blackPlayerId, result, whiteRanking, blackRanking, pgn);
}
