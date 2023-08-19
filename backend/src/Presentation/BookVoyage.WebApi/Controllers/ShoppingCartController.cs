using BookVoyage.Application.ShoppingCarts.Commands;
using BookVoyage.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookVoyage.WebApi.Controllers;

/// <summary>
/// Represents the shopping cart endpoint.
/// </summary>
public class ShoppingCartController: BaseApiController
{
    //  Insert or update a shopping cart
    [AllowAnonymous]
    [HttpPost(ApiEndpoints.ShoppingCart.UpsertItem)]
    public async Task<IActionResult> UpsertItemInCart(UpsertShoppingCartDto shoppingCartDto)
    {
        return HandleResult(await Mediator.Send(new UpsertShoppingCartCommand{ UpsertShoppingCartDto = shoppingCartDto }));
    }
}