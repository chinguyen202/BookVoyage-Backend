using AutoMapper;
using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Categories.Commands;

public record EditCategoryCommand: IRequest<ApiResult<Unit>>
{
    public Category Category { get; set; }
}

public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public EditCategoryCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<ApiResult<Unit>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.FindAsync(request.Category.Id);
        if (category == null) return null;
        _mapper.Map(request.Category, category);
        var result = await _dbContext.SaveChangesAsync() > 0;
        if (!result) return ApiResult<Unit>.Failure("Fail to update the category");
        return ApiResult<Unit>.Success(Unit.Value) ;
    }
}