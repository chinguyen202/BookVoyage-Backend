using BookVoyage.Persistence.Data;

namespace BookVoyage.Application.Authors.Validators;

public class AuthorCreateValidator: AuthorBaseValidator<AuthorDto>
{
    public AuthorCreateValidator(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}