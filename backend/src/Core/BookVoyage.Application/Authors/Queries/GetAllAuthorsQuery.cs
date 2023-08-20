using AutoMapper;
using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Application.Authors.Queries;


// Handles a query to fetch a list of categories from the database.
    public record GetAllAuthorsQuery : IRequest<ApiResult<List<AuthorDto>>>;

    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, ApiResult<List<AuthorDto>>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllAuthorsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResult<List<AuthorDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _dbContext.Authors.ToListAsync(cancellationToken);
            return ApiResult<List<AuthorDto>>.Success(_mapper.Map<List<AuthorDto>>(authors));
        }
    }

