using FluentValidation;

using BookVoyage.Application.Common.Interfaces;

namespace BookVoyage.Application.Books;

public class BookValidator: AbstractValidator<BookUpsertDto>
{
    private readonly IApplicationDbContext _dbContext;

    public BookValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(x => x.Title).NotEmpty().MinimumLength(2).MaximumLength(100);
        RuleFor(x => x.ISBN).NotEmpty().MinimumLength(10).MaximumLength(13);
        RuleFor(x => x.Summary).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.UnitPrice).NotEmpty().GreaterThan(0).LessThanOrEqualTo(1000);
        RuleFor(x => x.UnitInStock).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.YearOfPublished).LessThanOrEqualTo(DateTime.UtcNow.Year).GreaterThanOrEqualTo(1500);
        RuleFor(x => x.File).NotEmpty();
        
        // Apply BeUniqueBook rule only during creation (when Id is not set)
        RuleFor(x => x)
            .Must((dto, book) => dto.Id == Guid.Empty || BeUniqueBook(dto))
            .WithMessage("The book is already exists in the database.")
            .When(dto => dto.Id == Guid.Empty); // Only apply the rule when Id is empty
    }
    
    private bool BeUniqueBook(BookUpsertDto book)
    {
        var existingBooks = _dbContext.Books
            .Where(b => b.Title == book.Title 
            && b.Author.Id == book.AuthorId 
            && b.YearOfPublished == book.YearOfPublished)
            .ToList();

        return existingBooks.Count == 0;
    }
}
