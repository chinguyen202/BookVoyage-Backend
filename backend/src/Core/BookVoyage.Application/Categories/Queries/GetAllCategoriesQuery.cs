using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookVoyage.Application.Categories.Queries;


// Handles a query to fetch a list of categories from the database.
    public record GetAllCategoriesQuery : IRequest<List<Category>>;

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<Category>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAllCategoriesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.ToListAsync();
        }
    }

