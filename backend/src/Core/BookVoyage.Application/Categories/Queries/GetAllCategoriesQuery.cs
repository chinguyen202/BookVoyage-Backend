using AutoMapper;
using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Application.Categories.Queries;


// Handles a query to fetch a list of categories from the database.
    public record GetAllCategoriesQuery : IRequest<ApiResult<List<CategoryDto>>>;

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, ApiResult<List<CategoryDto>>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResult<List<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _dbContext.Categories.ToListAsync(cancellationToken);
            return ApiResult<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories));
        }
    }

