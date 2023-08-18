using BookVoyage.Domain.Entities;
using FluentValidation;

namespace BookVoyage.Application.Categories;

public class CategoryValidator: AbstractValidator<CategoryDto>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty(); 
    }
}