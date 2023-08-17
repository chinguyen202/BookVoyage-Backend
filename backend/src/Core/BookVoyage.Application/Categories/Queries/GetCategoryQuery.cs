using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Categories.Queries;

// Handles a query to fetch a category by ID from the database.
public record GetCategoryQuery : IRequest<ApiResult<Category>>
{
    public Guid Id { get; set; }
}

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ApiResult<Category>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetCategoryQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResult<Category>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
       var category = await _dbContext.Categories.FindAsync(request.Id);
       return ApiResult<Category>.Success(category);
    }
}