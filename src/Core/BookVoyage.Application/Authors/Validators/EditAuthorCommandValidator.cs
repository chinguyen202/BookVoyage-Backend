using FluentValidation;

using BookVoyage.Application.Authors.Commands;
using BookVoyage.Application.Common.Interfaces;

namespace BookVoyage.Application.Authors.Validators;

public class EditAuthorCommandValidator : AbstractValidator<EditAuthorCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public EditAuthorCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.AuthorEditDto.FullName).NotEmpty();
        RuleFor(x => x.AuthorEditDto.Publisher).NotEmpty();
        RuleFor(x => x.AuthorEditDto).Must(BeUniqueAuthor).WithMessage("Author with the same name and publisher already exists.");
    }

    private bool BeUniqueAuthor(AuthorEditDto authorEditDto)
    {
        var existingAuthors = _dbContext.Authors
            .Where(a => a.FullName == authorEditDto.FullName && a.Publisher == authorEditDto.Publisher)
            .ToList();

        // Exclude the current author being edited from the comparison
        existingAuthors = existingAuthors.Where(a => a.Id != authorEditDto.Id).ToList();

        return existingAuthors.Count == 0;
    }
}
