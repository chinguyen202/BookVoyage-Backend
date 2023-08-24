namespace BookVoyage.Application.Orders.Queries;

public class OrderItemDto
{
    public double Price { get; set; }
    public int Quantity { get; set; }
    public Guid BookId { get; set; }
    public string BookName { get; set; }
    public string BookImageUrl { get; set; }
}