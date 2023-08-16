using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookVoyage.WebApi.Controllers;

// BaseApiController for easy access to Mediator through IMediator service.
[ApiController]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}

