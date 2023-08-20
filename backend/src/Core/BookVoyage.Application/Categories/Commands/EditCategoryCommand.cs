using AutoMapper;
using BookVoyage.Application.Authors;
using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using FluentValidation;
using MediatR;

namespace BookVoyage.Application.Categories.Commands;

public record EditCategoryCommand: IRequest<ApiResult<Unit>>
{
    public CategoryDto CategoryUpdate { get; set; }
}

public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<CategoryDto> _validator;

    public EditCategoryCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<CategoryDto> validator)
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
        var result = _dbContext.SaveChangesAsync() ;
        return ApiResult<Unit>.Success(Unit.Value);
    }
}