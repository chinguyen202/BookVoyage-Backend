using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Application.Authors.Queries;


// Handles a query to fetch a list of categories from the database.
    public record GetAllAuthorsQuery : IRequest<ApiResult<List<Author>>>;

    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, ApiResult<List<Author>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAllAuthorsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResult<List<Author>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            return ApiResult<List<Author>>.Success(await _dbContext.Authors.ToListAsync());
        }
    }

