using BookVoyage.Application.Authors;
using BookVoyage.Application.Categories;
using Microsoft.AspNetCore.Http;

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
    public List<AuthorDto> Authors { get; set; }
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
    public IFormFile File { get; set; }
    public Guid CategoryId { get; set; }
    public List<Guid> AuthorIds { get; set; }
}