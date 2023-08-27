using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;

namespace BookVoyage.Application.Authors.Queries;


// Handles a query to fetch a list of categories from the database.
    public record GetAllAuthorsQuery : IRequest<ApiResult<List<AuthorEditDto>>>;

    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, ApiResult<List<AuthorEditDto>>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllAuthorsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResult<List<AuthorEditDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _dbContext.Authors.ToListAsync(cancellationToken);
            var result = _mapper.Map<List<AuthorEditDto>>(authors);
            return ApiResult<List<AuthorEditDto>>.Success(result);
        }
    }

