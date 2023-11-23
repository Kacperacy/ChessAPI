using Chess.Domain.Games.Entities;
using Chess.Domain.Games.Enums;
using Chess.Domain.Games.Exceptions;
using Chess.Domain.Games.Repositories;
using Chess.Domain.Identity.Entities;
using Chess.Infrastructure.EF.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.Games.Services;

public class GameRepository : IGameRepository
{
    private readonly EFContext _context;
    private readonly DbSet<Game> _games;
    private readonly UserManager<User> _userManager;
    
    public GameRepository(EFContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
        _games = _context.Games;
    }
    
    public async Task<Game?> GetAsync(Guid gameId, CancellationToken cancellationToken)
        => await _games.SingleOrDefaultAsync(game => game.Id == gameId, cancellationToken: cancellationToken);

    public async Task<Guid> CreateAsync(Guid? whitePlayerId, Guid? blackPlayerId, ResultEnum result, int? whiteRanking, int? blackRanking,
        string pgn, CancellationToken cancellationToken)
    {
        if (whitePlayerId is not null)
        {
            var whitePlayer = await _userManager.FindByIdAsync(whitePlayerId.ToString());
            
            if(whitePlayer is null)
                throw new UserNotFoundException();
        }
        
        if (blackPlayerId is not null)
        {
            var blackPlayer = await _userManager.FindByIdAsync(blackPlayerId.ToString());
            
            if(blackPlayer is null)
                throw new UserNotFoundException();
        }
        
        var game = Game.Create(whitePlayerId, blackPlayerId, result, whiteRanking, blackRanking, pgn);
        
        var record = await _games.AddAsync(game, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return record.Entity.Id;
    }

    public async Task UpdateAsync(Game game, CancellationToken cancellationToken)
    {
        _games.Update(game);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Game game, CancellationToken cancellationToken)
    {
        _games.Remove(game);
        await _context.SaveChangesAsync(cancellationToken);
    }
}