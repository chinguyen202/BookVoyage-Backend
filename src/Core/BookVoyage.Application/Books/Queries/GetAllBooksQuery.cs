using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;

namespace BookVoyage.Application.Books.Queries;


// Handles a query to fetch a list of categories from the database.
    public record GetAllBooksQuery : IRequest<ApiResult<List<BookDto>>>;

    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, ApiResult<List<BookDto>>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllBooksQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<BookDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _dbContext.Books
                .Include(a => a.Category)
                .Include(a => a.Author)
                .ToListAsync(cancellationToken: cancellationToken);

            var bookDtos = _mapper.Map<List<BookDto>>(books);
            return ApiResult<List<BookDto>>.Success(bookDtos);
        }
    }

