using BookVoyage.Domain.Entities;
using FluentValidation;

namespace BookVoyage.Application.Categories;

public class CategoryValidator: AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty(); 
    }
}