using Chess.Domain.Common.Pagination;
using MediatR;

namespace Chess.Application.Games.Queries.BrowseGames;

public sealed record BrowseGamesQuery(Guid? PlayerId) : PaginationRequest, IRequest<BrowseGamesResponse>;