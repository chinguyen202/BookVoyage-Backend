using BookVoyage.Domain.Common;

namespace BookVoyage.Domain.Entities;

public class Author: AuditableBaseEntity
{
    public string FullName { get; set; }
    public string Publisher { get; set; }
    public List<Book> Books { get; set; } = new();
}