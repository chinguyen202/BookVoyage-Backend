using BookVoyage.Domain.Entities;
using FluentValidation;

namespace BookVoyage.Application.Books;

public class BookValidator: AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Category).NotEmpty();
        RuleFor(x => x.Publisher).NotEmpty();
        RuleFor(x => x.Summary).NotEmpty();
        RuleFor(x => x.YearOfPublished).LessThanOrEqualTo(DateTime.UtcNow.Year);
    }
}
