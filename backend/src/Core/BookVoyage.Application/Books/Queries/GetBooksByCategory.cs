using MediatR;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;
using BookVoyage.Domain.Entities;

namespace BookVoyage.Application.Books.Queries;

// Handles a query to fetch list of book by category ID from the database.
public record GetBooksByCategoryQuery : IRequest<ApiResult<List<Book>>>
{
    public Guid Id { get; set; }
}

public class GetBooksByCategoryHandler : IRequestHandler<GetBooksByCategoryQuery, ApiResult<List<Book>>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetBooksByCategoryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResult<List<Book>>> Handle(GetBooksByCategoryQuery request, CancellationToken cancellationToken)
    {
        var books = await _dbContext.Books.Where(c => c.CategoryId == request.Id).ToListAsync();
        return ApiResult<List<Book>>.Success(books);
    }
}