using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using BookVoyage.Application.ShoppingCarts.Commands;
using BookVoyage.Application.ShoppingCarts.Queries;
using BookVoyage.Utility.Constants;

namespace BookVoyage.WebApi.Controllers;

/// <summary>
/// Represents the shopping cart endpoint.
/// </summary>
public class ShoppingCartController: BaseApiController
{
    // Get shopping cart of a user
    [HttpGet(ApiEndpoints.V1.ShoppingCart.Get)]
    public async Task<IActionResult> GetCart()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return HandleResult(await Mediator.Send(new GetCartQuery { Id = id }));
    }
    
    //  Insert or update a shopping cart
    [HttpPost(ApiEndpoints.V1.ShoppingCart.UpsertItem)]
    public async Task<IActionResult> UpsertItemInCart(UpsertShoppingCartDto shoppingCartDto)
    {
        return HandleResult(await Mediator.Send(new UpsertShoppingCartCommand{ UpsertShoppingCartDto = shoppingCartDto }));
    }
    
}