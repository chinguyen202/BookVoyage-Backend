using FluentValidation;
using BookVoyage.Persistence.Data;

namespace BookVoyage.Application.Authors.Validators;

public abstract class AuthorBaseValidator<T> : AbstractValidator<T>
    where T : AuthorDto
{
    protected readonly ApplicationDbContext _dbContext;

    protected AuthorBaseValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Publisher).NotEmpty();
        RuleFor(x => x).Must(BeUniqueAuthor).WithMessage("Author with the same name and publisher already exists.");
    }

    protected bool BeUniqueAuthor(T author)
    {
        var existingAuthors = _dbContext.Authors
            .Where(a => a.FirstName == author.FirstName && a.LastName == author.LastName && a.Publisher == author.Publisher)
            .ToList();
        return existingAuthors.Count == 0;
    }
}

