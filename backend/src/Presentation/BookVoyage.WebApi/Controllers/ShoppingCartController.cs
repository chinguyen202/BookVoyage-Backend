using BookVoyage.Application.ShoppingCarts.Commands;
using BookVoyage.Application.ShoppingCarts.Queries;
using BookVoyage.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookVoyage.WebApi.Controllers;

/// <summary>
/// Represents the shopping cart endpoint.
/// </summary>
public class ShoppingCartController: BaseApiController
{
    // Get shopping cart of a user
    [AllowAnonymous] // for development only
    [HttpGet(ApiEndpoints.ShoppingCart.Get)]
    public async Task<IActionResult> GetCart(string id)
    {
        return HandleResult(await Mediator.Send(new GetCartQuery { Id = id }));
    }
    
    //  Insert or update a shopping cart
    [AllowAnonymous] // for development only
    [HttpPost(ApiEndpoints.ShoppingCart.UpsertItem)]
    public async Task<IActionResult> UpsertItemInCart(UpsertShoppingCartDto shoppingCartDto)
    {
        return HandleResult(await Mediator.Send(new UpsertShoppingCartCommand{ UpsertShoppingCartDto = shoppingCartDto }));
    }
    
}