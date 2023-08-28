using System.ComponentModel.DataAnnotations.Schema;
using BookVoyage.Domain.Common;

namespace BookVoyage.Domain.Entities.OrderAggegate;

public class OrderItem: BaseEntity
{
    public double Price { get; set; }
    public int Quantity { get; set; }
    public BookOrderedItem BookOrderedItem { get; set; }
}