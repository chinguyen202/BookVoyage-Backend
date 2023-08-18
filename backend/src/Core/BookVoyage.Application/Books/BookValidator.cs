using BookVoyage.Domain.Entities;
using FluentValidation;

namespace BookVoyage.Application.Books;

public class BookValidator: AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MinimumLength(2).MaximumLength(100);
        RuleFor(x => x.ISBN).NotEmpty().MinimumLength(10).MaximumLength(13);
        RuleFor(x => x.Summary).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.AuthorId).NotEmpty();
        RuleFor(x => x.Publisher).NotEmpty();
        RuleFor(x => x.UnitPrice).NotEmpty().GreaterThan(0).LessThanOrEqualTo(1000);
        RuleFor(x => x.UnitInStock).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.YearOfPublished).LessThanOrEqualTo(DateTime.UtcNow.Year).GreaterThanOrEqualTo(1500);
    }
}
