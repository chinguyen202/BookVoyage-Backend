using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Application.Categories.Queries;


// Handles a query to fetch a list of categories from the database.
    public record GetAllCategoriesQuery : IRequest<ApiResult<List<Category>>>;

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, ApiResult<List<Category>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAllCategoriesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResult<List<Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return ApiResult<List<Category>>.Success(await _dbContext.Categories.ToListAsync());
        }
    }

