using AutoMapper;
using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using FluentValidation;
using MediatR;

namespace BookVoyage.Application.Categories.Commands;

public record CreateCategoryCommand : IRequest<ApiResult<Unit>>
{
    public CategoryDto CategoryDto { get; set; }
}


public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<CategoryDto> _validator;

    public CreateCategoryCommandHandler(ApplicationDbContext dbContext, IMapper mapper, CategoryValidator validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<ApiResult<Unit>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.CategoryDto, cancellationToken);
        var category = _mapper.Map<Category>(request.CategoryDto);
        _dbContext.Categories.Add(category);
        var result = await _dbContext.SaveChangesAsync() > 0;
        if (!result) return ApiResult<Unit>.Failure("Failed to create Category");
        return ApiResult<Unit>.Success(Unit.Value);
    }
}