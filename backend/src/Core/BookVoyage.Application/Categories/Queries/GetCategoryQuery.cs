using AutoMapper;
using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Categories.Queries;

// Handles a query to fetch a category by ID from the database.
public record GetCategoryQuery : IRequest<ApiResult<CategoryDto>>
{
    public Guid Id { get; set; }
}

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ApiResult<CategoryDto>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResult<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
       var category = await _dbContext.Categories
           .FindAsync(request.Id);
       return ApiResult<CategoryDto>.Success(_mapper.Map<CategoryDto>(category));
    }
}