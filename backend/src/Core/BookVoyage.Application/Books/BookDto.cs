using BookVoyage.Domain.Entities;

namespace BookVoyage.Application.Books;

public class BookDto
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public double UnitPrice { get; set; }
    public int UnitInStock { get; set; }
    public string Summary { get; set; }
    public string Publisher { get; set; }
    public int YearOfPublished { get; set; }
    public string ImageUrl { get; set; }
    public Guid CategoryId { get; set; }
    public Guid AuthorId { get; set; }
}

public class BookEditDto: BookDto
{
    public Guid Id { get; set; }
}