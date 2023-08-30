using Bogus;
using BookVoyage.Application.Authors;
using BookVoyage.Application.Categories;

namespace BookVoyage.Tests.Integration;

public class TestDataGenerator
{
    private readonly Faker<CategoryDto> _categoryFaker;
    private readonly Faker<AuthorEditDto> _authorEditFaker;
    private readonly Faker<AuthorDto> _authorFaker;

    public TestDataGenerator()
    {
        _categoryFaker = new Faker<CategoryDto>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.Name, f => f.Commerce.ProductName());

        _authorEditFaker = new Faker<AuthorEditDto>()
            .RuleFor(b => b.Id, f => f.Random.Guid())
            .RuleFor(a => a.FullName, f => f.Name.FullName())
            .RuleFor(a => a.Publisher, f => f.Company.CompanyName());
        _authorFaker = new Faker<AuthorDto>()
            .RuleFor(a => a.FullName, f => f.Name.FullName())
            .RuleFor(a => a.Publisher, f => f.Company.CompanyName());
    }

    public CategoryDto GenerateCategory()
    {
        return _categoryFaker.Generate();
    }

    public AuthorDto GenerateEditAuthor()
    {
        return _authorEditFaker.Generate();
    }

    public AuthorDto GenerateAuthor()
    {
        return _authorFaker.Generate();
    }
}