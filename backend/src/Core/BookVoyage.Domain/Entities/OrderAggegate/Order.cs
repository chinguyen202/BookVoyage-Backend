using System.ComponentModel.DataAnnotations.Schema;
using BookVoyage.Domain.Common;

namespace BookVoyage.Domain.Entities.OrderAggegate;

public class Order: AuditableBaseEntity
{
    
    public double Subtotal { get; set; }
    public int TotalQuantity { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string StripePaymentIntentId { get; set; }
    public ShippingAddress ShippingAddress { get; set; }
    public string BuyerId { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}