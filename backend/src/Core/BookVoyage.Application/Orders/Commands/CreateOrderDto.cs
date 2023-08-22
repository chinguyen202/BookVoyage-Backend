using BookVoyage.Domain.Entities.OrderAggegate;

namespace BookVoyage.Application.Orders.Commands;

public class CreateOrderDto
{
    public bool SaveAddress { get; set; }
    public ShippingAddress ShippingAddress { get; set; }
}