using BookVoyage.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookVoyage.WebApi.Controllers;

// BaseApiController for easy access to Mediator through IMediator service.
[ApiController]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected ActionResult HandleResult<T>(ApiResult<T> result)
    {
        if (result == null) return NotFound();
        if (result.IsSuccess && result.Value != null)
            return Ok(result.Value);
        if (result.IsSuccess && result.Value == null)
            return NotFound();
        return BadRequest(result.Error);
    }
}

