using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using BookVoyage.Application.Authors;
using BookVoyage.Application.Authors.Commands;
using BookVoyage.Application.Authors.Queries;
using BookVoyage.Application.Categories.Commands;
using BookVoyage.Utility.Constants;

namespace BookVoyage.WebApi.Controllers;

/// <summary>
/// Author endpoint
/// </summary>
public class AuthorsController: BaseApiController
{
    //  Get all Author
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.Authors.GetAll)]
    public async Task<IActionResult> GetAuthors()
    {
        return HandleResult(await Mediator.Send(new GetAllAuthorsQuery())) ;
    }
    //  Get a specific Author
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.Authors.Get)]
    public async Task<IActionResult> GetAuthor(Guid id)
    {
        return HandleResult(await Mediator.Send(new GetAuthorQuery { Id = id }));
    }
    //  Create a Author
    [AllowAnonymous] // For development only
    [HttpPost(ApiEndpoints.Authors.Create)]
    public async Task<IActionResult> CreateAuthor(AuthorDto authorDto)
    {
        return HandleResult(await Mediator.Send(new CreateAuthorCommand { AuthorDto = authorDto }));
    }
    
    // Update a Author
    [AllowAnonymous] // For development only
    [HttpPut(ApiEndpoints.Authors.Update)]
    public async Task<IActionResult> EditAuthor(Guid id, AuthorEditDto authorEditDto)
    {
        authorEditDto.Id = id;
        return HandleResult(await Mediator.Send(new EditAuthorCommand { AuthorEditDto = authorEditDto }));
    }
    
    // Delete a Author
    [AllowAnonymous] // For development only
    [HttpDelete(ApiEndpoints.Authors.Delete)]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteAuthorCommand{Id = id}));
    }
}