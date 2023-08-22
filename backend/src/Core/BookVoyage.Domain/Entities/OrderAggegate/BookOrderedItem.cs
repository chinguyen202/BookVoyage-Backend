using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Domain.Entities.OrderAggegate;

[Owned]
public class BookOrderedItem
{
    public Guid BookId { get; set; }
    public string BookName { get; set; }
    public string ImageUrl { get; set; }
}