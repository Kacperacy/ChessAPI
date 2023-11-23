using Chess.Domain.Games.Entities;
using Microsoft.AspNetCore.Identity;

namespace Chess.Domain.Identity.Entities;

public sealed class User : IdentityUser<Guid>
{
    public int Ranking { get; private set; } = 1000;
    public int Wins { get; private set; } = 0;
    public int Loses { get; private set; } = 0;
    public int Draws { get; private set; } = 0;

    private readonly List<Game> _gamesAsWhite = new();
    public IReadOnlyCollection<Game> GamesAsWhite => _gamesAsWhite;
    
    private readonly List<Game> _gamesAsBlack = new();
    public IReadOnlyCollection<Game> GamesAsBlack => _gamesAsBlack;
}