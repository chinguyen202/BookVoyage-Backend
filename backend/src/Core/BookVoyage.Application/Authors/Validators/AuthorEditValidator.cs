using BookVoyage.Persistence.Data;
using FluentValidation;

namespace BookVoyage.Application.Authors.Validators;

public class AuthorEditValidator: AuthorBaseValidator<AuthorDto>
{
    public AuthorEditValidator(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}