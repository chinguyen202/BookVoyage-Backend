using BookVoyage.Domain.Entities.OrderAggegate;

namespace BookVoyage.Application.Orders.Commands;

public class OrderUpdatedDto
{
    public Guid OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
}