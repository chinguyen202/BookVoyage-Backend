using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Application.Books.Queries;

// Handles a query to fetch list of book by category ID from the database.
public record GetBooksByCategoryQuery : IRequest<ApiResult<List<Book>>>
{
    public Guid Id { get; set; }
}

public class GetBooksByCategoryHandler : IRequestHandler<GetBooksByCategoryQuery, ApiResult<List<Book>>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetBooksByCategoryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResult<List<Book>>> Handle(GetBooksByCategoryQuery request, CancellationToken cancellationToken)
    {
        var books = await _dbContext.Books.Where(c => c.CategoryId == request.Id).ToListAsync();
        return ApiResult<List<Book>>.Success(books);
    }
}