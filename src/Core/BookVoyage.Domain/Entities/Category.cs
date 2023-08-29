using BookVoyage.Domain.Common;

namespace BookVoyage.Domain.Entities;

public class Category: AuditableBaseEntity
{
    public string Name { get; set; }
    public List<Book> Books { get; set; }
}