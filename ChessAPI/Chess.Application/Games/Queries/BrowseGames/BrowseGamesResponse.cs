using Chess.Application.Games.DTO;
using Chess.Domain.Common.Pagination;

namespace Chess.Application.Games.Queries.BrowseGames;

public sealed record BrowseGamesResponse(PaginatedList<GameDto> Games);