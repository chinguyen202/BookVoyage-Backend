using BookVoyage.Domain.Common;

namespace BookVoyage.Domain.Entities;

public class Author: AuditableBaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Publisher { get; set; }
    public List<Book> Books { get; set; } = new();
}