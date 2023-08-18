using BookVoyage.Application.Authors.Queries;
using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Books.Queries;

// Handles a query to fetch a category by ID from the database.
public record GetBookQuery : IRequest<ApiResult<Book>>
{
    public Guid Id { get; set; }
}

public class GetBookQueryHandler : IRequestHandler<GetBookQuery, ApiResult<Book>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetBookQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResult<Book>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
       var book = await _dbContext.Books.FindAsync(request.Id);
       return ApiResult<Book>.Success(book);
    }
}