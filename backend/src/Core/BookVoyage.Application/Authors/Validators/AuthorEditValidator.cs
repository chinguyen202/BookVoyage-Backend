using BookVoyage.Persistence.Data;
using FluentValidation;

namespace BookVoyage.Application.Authors.Validators;

public class AuthorEditValidator: AuthorBaseValidator<AuthorEditDto>
{
    public AuthorEditValidator(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}