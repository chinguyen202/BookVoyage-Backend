using AutoMapper;
using MediatR;

using BookVoyage.Application.Common;
using BookVoyage.Persistence.Data;

namespace BookVoyage.Application.Categories.Queries;

// Handles a query to fetch a category by ID from the database.
public record GetCategoryQuery : IRequest<ApiResult<CategoryDto>>
{
    public Guid Id { get; set; }
}

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ApiResult<CategoryDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResult<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
       var category = await _dbContext.Categories
           .FindAsync(request.Id);
       if (category == null)
       {
           return ApiResult<CategoryDto>.Failure("Can't find the category requested");
       }
       return ApiResult<CategoryDto>.Success(_mapper.Map<CategoryDto>(category));
    }
}