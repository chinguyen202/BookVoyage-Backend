using System.ComponentModel.DataAnnotations.Schema;
using BookVoyage.Domain.Common;

namespace BookVoyage.Domain.Entities;

public class ShoppingCart: AuditableBaseEntity
{
    public string BuyerId { get; set; }
    public List<CartItem> CartItems { get; set; } = new();
    [NotMapped]
    public double CartTotal { get; set; }
    // Stripe
    [NotMapped]
    public string StripePaymentIntended { get; set; }
    [NotMapped]
    public string ClientSecret { get; set; }
}

