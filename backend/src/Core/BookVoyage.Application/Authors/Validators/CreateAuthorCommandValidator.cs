using FluentValidation;

using BookVoyage.Application.Authors.Commands;
using BookVoyage.Persistence.Data;

namespace BookVoyage.Application.Authors.Validators;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    private IApplicationDbContext _dbContext;

    public CreateAuthorCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.AuthorDto.FullName).NotEmpty();
        RuleFor(x => x.AuthorDto.Publisher).NotEmpty();
        RuleFor(x => x.AuthorDto).Must(BeUniqueAuthor).WithMessage("Author with the same name and publisher already exists.");
    }

    private bool BeUniqueAuthor(AuthorDto authorCreateDto)
    {
        var existingAuthors = _dbContext.Authors
            .Where(a => a.FullName == authorCreateDto.FullName && a.Publisher == authorCreateDto.Publisher)
            .ToList();
        return existingAuthors.Count == 0;
    }
}