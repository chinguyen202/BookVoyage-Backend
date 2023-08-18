using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Application.Books.Queries;

// Handles a query to fetch list of book by author ID from the database.
public record GetBooksByAuthorQuery : IRequest<ApiResult<List<Book>>>
{
    public Guid Id { get; set; }
}

public class GetBooksByAuthorHandler : IRequestHandler<GetBooksByAuthorQuery, ApiResult<List<Book>>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetBooksByAuthorHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    

    public async Task<ApiResult<List<Book>>> Handle(GetBooksByAuthorQuery request, CancellationToken cancellationToken)
    {
        var books = await _dbContext.Books.Where(c => c.AuthorId == request.Id).ToListAsync();
        return ApiResult<List<Book>>.Success(books);
    }
}