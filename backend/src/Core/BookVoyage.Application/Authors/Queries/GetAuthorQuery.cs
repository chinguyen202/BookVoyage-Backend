using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Authors.Queries;

// Handles a query to fetch a category by ID from the database.
public record GetAuthorQuery : IRequest<ApiResult<Author>>
{
    public Guid Id { get; set; }
}

public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, ApiResult<Author>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAuthorQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResult<Author>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
       var author = await _dbContext.Authors.FindAsync(request.Id);
       return ApiResult<Author>.Success(author);
    }
}