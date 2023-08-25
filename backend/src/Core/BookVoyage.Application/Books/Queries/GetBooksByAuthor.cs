using MediatR;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;
using BookVoyage.Domain.Entities;

namespace BookVoyage.Application.Books.Queries;

// Handles a query to fetch list of book by author ID from the database.
public record GetBooksByAuthorQuery : IRequest<ApiResult<List<Book>>>
{
    public Guid Id { get; set; }
}

public class GetBooksByAuthorHandler : IRequestHandler<GetBooksByAuthorQuery, ApiResult<List<Book>>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetBooksByAuthorHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    

    public async Task<ApiResult<List<Book>>> Handle(GetBooksByAuthorQuery request, CancellationToken cancellationToken)
    {
        var books = await _dbContext.Books
            .Where(b => b.Authors.Any(a => a.Id == request.Id))
            .Include(b => b.Category)
            .Include(b => b.Authors)
            .ToListAsync(cancellationToken: cancellationToken);
        return ApiResult<List<Book>>.Success(books);
    }
}