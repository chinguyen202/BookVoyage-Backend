using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using BookVoyage.Application.Books;
using BookVoyage.Application.Books.Commands;
using BookVoyage.Application.Books.Queries;
using BookVoyage.Utility.Constants;

namespace BookVoyage.WebApi.Controllers;

/// <summary>
/// Controller for all book-related operations
/// </summary>
public class BooksController: BaseApiController
{
    //  Get all Book
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.V1.Books.GetAll)]
    public async Task<IActionResult> GetAllBook()
    {
        return HandleResult(await Mediator.Send(new GetAllBooksQuery())) ;
    }
    //  Get a specific Book
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.V1.Books.Get)]
    public async Task<IActionResult> GetBook(Guid id)
    {
        return HandleResult(await Mediator.Send(new GetBookQuery { Id = id }));
    }
    //  Create a Book
    [Authorize(Roles = SD.Admin)]
    [HttpPost(ApiEndpoints.V1.Books.Create)]
    public async Task<IActionResult> CreateBook([FromForm]BookUpsertDto bookDto)
    {
        return HandleResult(await Mediator.Send(new CreateBookCommand { BookUpsertDto = bookDto }));
    }
    
    // Update a Book
    [Authorize(Roles = SD.Admin)]
    [HttpPut(ApiEndpoints.V1.Books.Update)]
    public async Task<IActionResult> EditBook(Guid id, [FromForm]BookUpsertDto bookEditDto)
    {
        bookEditDto.Id = id;
        return HandleResult(await Mediator.Send(new EditBookCommand { BookEditDto = bookEditDto }));
    }
    
    // Delete a Book
    [Authorize(Roles = SD.Admin)]
    [HttpDelete(ApiEndpoints.V1.Books.Delete)]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteBookCommand{Id = id}));
    }
    
    // Get list of books by category
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.V1.Books.GetByCategory)]
    public async Task<IActionResult> GetByCategory(Guid categoryId)
    {
        return HandleResult(await Mediator.Send(new GetBooksByCategoryQuery { Id = categoryId }));
    }
    
    // Get list of books by author
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.V1.Books.GetByAuthor)]
    public async Task<IActionResult> GetByAuthor(Guid authorId)
    {
        return HandleResult(await Mediator.Send(new GetBooksByAuthorQuery { Id = authorId }));
    }
}