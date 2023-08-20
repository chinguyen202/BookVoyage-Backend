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
    [HttpGet(ApiEndpoints.Books.GetAll)]
    public async Task<IActionResult> GetAllBook()
    {
        return HandleResult(await Mediator.Send(new GetAllBooksQuery())) ;
    }
    //  Get a specific Book
    [AllowAnonymous]
    [HttpGet("api/books/{id}")]
    public async Task<IActionResult> GetBook(Guid id)
    {
        return HandleResult(await Mediator.Send(new GetBookQuery { Id = id }));
    }
    //  Create a Book
    [AllowAnonymous]
    [HttpPost(ApiEndpoints.Books.Create)]
    public async Task<IActionResult> CreateBook(BookUpsertDto bookDto)
    {
        return HandleResult(await Mediator.Send(new CreateBookCommand { BookUpsertDto = bookDto }));
    }
    
    // Update a Book
    [AllowAnonymous]
    [HttpPut(ApiEndpoints.Books.Update)]
    public async Task<IActionResult> EditBook(Guid id, BookUpsertDto bookEditDto)
    {
        bookEditDto.Id = id;
        return HandleResult(await Mediator.Send(new EditBookCommand { BookEditDto = bookEditDto }));
    }
    
    // Delete a Book
    [AllowAnonymous]
    [HttpDelete(ApiEndpoints.Books.Delete)]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteBookCommand{Id = id}));
    }
    
    // Get list of books by category
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.Books.GetByCategory)]
    public async Task<IActionResult> GetByCategory(Guid categoryId)
    {
        return HandleResult(await Mediator.Send(new GetBooksByCategoryQuery { Id = categoryId }));
    }
    
    // Get list of books by author
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.Books.GetByAuthor)]
    public async Task<IActionResult> GetByAuthor(Guid authorId)
    {
        return HandleResult(await Mediator.Send(new GetBooksByAuthorQuery { Id = authorId }));
    }
}