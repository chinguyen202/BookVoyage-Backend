namespace BookVoyage.Application.ShoppingCarts.Commands;

public class UpsertShoppingCartDto
{
    public string BuyerId { get; set; }
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
}