using Chess.Application.Games.Queries.BrowseGames;
using Chess.Domain.Common.Pagination;
using Chess.Infrastructure.EF.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.Games.Queries.BrowseGames;

public class BrowseGamesHandler : IRequestHandler<BrowseGamesQuery, BrowseGamesResponse>
{
    private readonly EFContext _context;

    public BrowseGamesHandler(EFContext context)
    {
        _context = context;
    }

    public async Task<BrowseGamesResponse> Handle(BrowseGamesQuery query, CancellationToken cancellationToken)
    {
        var games = _context.Games.AsNoTracking();
        
        if (query.PlayerId.HasValue)
        {
            games = games.Where(game => game.WhitePlayerId == query.PlayerId || game.BlackPlayerId == query.PlayerId);
        }
        
        var result = await games
            .Select(game => game.AsDto())
            .ToPaginatedListAsync(query);
        
        return new BrowseGamesResponse(result);
    }
}