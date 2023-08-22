namespace BookVoyage.Domain.Entities.OrderAggegate;

public enum OrderStatus
{
    Pending,
    Processed,
    Confirmed,
    Cancelled
}