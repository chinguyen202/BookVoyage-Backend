using BookVoyage.Domain.Entities.OrderAggegate;

namespace BookVoyage.Application.Orders.Queries;

public class OrderDto
{
    public Guid Id { get; set; }
    public double Subtotal { get; set; }
    public int TotalQuantity { get; set; }
    public string OrderStatus { get; set; }
    public string StripePaymentIntentId { get; set; }
    public ShippingAddress ShippingAddress { get; set; }
    public string BuyerId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
}