using AutoMapper;
using FluentValidation;
using MediatR;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;

namespace BookVoyage.Application.Categories.Commands;

public record EditCategoryCommand: IRequest<ApiResult<Unit>>
{
    public CategoryDto CategoryUpdate { get; set; }
}

public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, ApiResult<Unit>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<CategoryDto> _validator;

    public EditCategoryCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IValidator<CategoryDto> validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ApiResult<Unit>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        // Find the category by its Id
        var category = await _dbContext.Categories.FindAsync(request.CategoryUpdate.Id);
        if (category == null)
        {
            return ApiResult<Unit>.Failure("Category doesn't exist!");
        }
        await _validator.ValidateAndThrowAsync(request.CategoryUpdate, cancellationToken);
        category.Name = request.CategoryUpdate.Name;
        var result = _dbContext.SaveChangesAsync(cancellationToken) ;
        return ApiResult<Unit>.Success(Unit.Value);
    }
}