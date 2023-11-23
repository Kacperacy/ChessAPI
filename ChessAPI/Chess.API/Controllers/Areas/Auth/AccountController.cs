using Chess.Application.Identity.Commands.SignIn;
using Chess.Application.Identity.Commands.SignUp;
using Chess.Domain.Identity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Chess.API.Controllers.Areas.Auth;

[Route($"{Endpoints.BaseUrl}/account")]
public class AccountController : BaseController
{
    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command, CancellationToken cancellationToken)
    {
        var token = await Mediator.Send(command, cancellationToken);
        return Created(string.Empty, token);
    }
    
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JsonWebToken>> SignIn([FromBody] SignInCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}