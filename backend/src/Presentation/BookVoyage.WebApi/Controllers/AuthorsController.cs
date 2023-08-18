using BookVoyage.Application.Authors;
using BookVoyage.Application.Authors.Commands;
using BookVoyage.Application.Authors.Queries;
using BookVoyage.Application.Categories.Commands;
using BookVoyage.Application.Categories.Queries;
using BookVoyage.Domain.Entities;
using BookVoyage.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookVoyage.WebApi.Controllers;

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
    [HttpPost(ApiEndpoints.Authors.Create)]
    public async Task<IActionResult> CreateAuthor(AuthorDto authorDto)
    {
        return HandleResult(await Mediator.Send(new CreateAuthorCommand { AuthorDto = authorDto }));
    }
    
    // Update a Author
    [HttpPut(ApiEndpoints.Authors.Update)]
    public async Task<IActionResult> EditAuthor(Guid id, AuthorEditDto authorEditDto)
    {
        authorEditDto.Id = id;
        return HandleResult(await Mediator.Send(new EditAuthorCommand { AuthorEditDto = authorEditDto }));
    }
    
    // Delete a Author
    [HttpDelete(ApiEndpoints.Authors.Delete)]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteAuthorCommand{Id = id}));
    }
}