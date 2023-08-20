using AutoMapper;
using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
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

    public EditCategoryCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<ApiResult<Unit>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        
        // Find the category by its Id
        var category = await _dbContext.Categories.FindAsync(request.CategoryUpdate.Id);
        if (category == null)
        {
            return ApiResult<Unit>.Failure("Category not found");
        }

        _mapper.Map(request.CategoryUpdate, category);
    
        try
        {
            // Save changes to the database
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            if (result > 0)
            {
                return ApiResult<Unit>.Success(Unit.Value);
            }
            else
            {
                return ApiResult<Unit>.Failure("Failed to update the category");
            }
        }
        catch (Exception ex)
        {
            return ApiResult<Unit>.Failure($"An error occurred: {ex.Message}");
        }
    }
}