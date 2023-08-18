using BookVoyage.Domain.Entities;
using FluentValidation;

namespace BookVoyage.Application.Authors;

public class AuthorValidator: AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty(); 
        RuleFor(x => x.LastName).NotEmpty();
    }
}