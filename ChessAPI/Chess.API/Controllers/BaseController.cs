using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chess.API.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
    protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
    {
        return result is null ? NotFound() : Ok(result);
    }
}