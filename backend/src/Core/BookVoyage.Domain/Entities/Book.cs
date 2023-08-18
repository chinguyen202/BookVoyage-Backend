using System.ComponentModel.DataAnnotations.Schema;
using BookVoyage.Domain.Common;

namespace BookVoyage.Domain.Entities;

public class Book: AuditableBaseEntity
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public double UnitPrice { get; set; }
    public int UnitInStock { get; set; }
    public string Summary { get; set; }
    public string Publisher { get; set; }
    public int YearOfPublished { get; set; }
    public string ImageUrl { get; set; }
    // Navigation property
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public List<Author> Authors { get; set; }
}