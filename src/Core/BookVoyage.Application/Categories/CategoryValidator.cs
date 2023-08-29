using BookVoyage.Application.Common.Interfaces;
using FluentValidation;

namespace BookVoyage.Application.Categories;

public class CategoryValidator: AbstractValidator<CategoryDto>
{
    private readonly IApplicationDbContext _dbContext;
    public CategoryValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters.")
            .Must(BeUniqueName).WithMessage("Category name already exist in database.");
    }
    private bool BeUniqueName(string name)
    {
        // Check if the name is unique within the database
        var existingCategory = _dbContext.Categories.FirstOrDefault(c => c.Name == name);
        return existingCategory == null;
    }
}