using BookVoyage.Domain.Entities;

namespace BookVoyage.Application.ShoppingCarts.Queries;

public class ShoppingCartResponseDto
{
    public string BuyerId { get; set; }
    public List<CartItem> CartItems { get; set; }
    public double CartTotal { get; set; }
}