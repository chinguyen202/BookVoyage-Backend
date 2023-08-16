using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Categories.Queries;

// Handles a query to fetch a category by ID from the database.
public record GetCategoryQuery : IRequest<Category>
{
    public Guid Id { get; set; }
}

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Category>
{
    private readonly ApplicationDbContext _dbContext;

    public GetCategoryQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Categories.FindAsync(request.Id);
    }
}