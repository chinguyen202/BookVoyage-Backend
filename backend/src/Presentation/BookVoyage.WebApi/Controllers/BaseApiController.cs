using Microsoft.AspNetCore.Mvc;
using BookVoyage.Utility;

namespace BookVoyage.WebApi.Controllers;

[ApiController]
// [Route($"{}/[controller]")]
public abstract class BaseApiController: ControllerBase
{
}