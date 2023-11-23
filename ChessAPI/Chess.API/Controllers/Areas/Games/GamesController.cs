using Chess.Application.Games.Commands.CreateGame;
using Chess.Application.Games.Queries.BrowseGames;
using Microsoft.AspNetCore.Mvc;

namespace Chess.API.Controllers.Areas.Games;

[Route($"{Endpoints.BaseUrl}/games")]
public class GamesController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BrowseGamesResponse>> BrowseGames([FromQuery] BrowseGamesQuery query, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(query, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameCommand command, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Created(string.Empty, result);
    }
}