using BookVoyage.Application.Authors.Queries;
using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Application.Books.Queries;


// Handles a query to fetch a list of categories from the database.
    public record GetAllBooksQuery : IRequest<ApiResult<List<Book>>>;

    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, ApiResult<List<Book>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAllBooksQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResult<List<Book>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return ApiResult<List<Book>>.Success(await _dbContext.Books.ToListAsync());
        }
    }

