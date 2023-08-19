using System.ComponentModel.DataAnnotations.Schema;
using BookVoyage.Domain.Common;

namespace BookVoyage.Domain.Entities;

public class CartItem
{
    public int Quantity { get; set; }
    // Navigation properties
    public Guid ShoppingCartId { get; set; }
    public Guid BookId { get; set; }
    [ForeignKey("BookId")]
    public Book Book { get; set; }
}