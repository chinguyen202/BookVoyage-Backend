using BookVoyage.Application.Authors;
using BookVoyage.Application.Categories;
using BookVoyage.Domain.Entities;

namespace BookVoyage.Application.Books;

public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public double UnitPrice { get; set; }
    public int UnitInStock { get; set; }
    public string Summary { get; set; }
    public int YearOfPublished { get; set; }
    public string ImageUrl { get; set; }
    public CategoryDto Category { get; set; }
    public List<AuthorEditDto> Authors { get; set; }
}

public class BookUpsertDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public double UnitPrice { get; set; }
    public int UnitInStock { get; set; }
    public string Summary { get; set; }
    public int YearOfPublished { get; set; }
    public string ImageUrl { get; set; }
    public Guid CategoryId { get; set; }
    public List<Guid> AuthorIds { get; set; }
}