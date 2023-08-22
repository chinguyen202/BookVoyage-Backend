using BookVoyage.Application.Orders.Commands;
using BookVoyage.Application.Orders.Queries;
using BookVoyage.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookVoyage.WebApi.Controllers;

/// <summary>
/// 
/// </summary>
public class OrdersController: BaseApiController
{
    // Get all orders of user
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.Orders.GetByUserId)]
    public async Task<IActionResult> GetOrders(string? id)
    {
        return HandleResult(await Mediator.Send(new GetOrdersQuery {UserId = id}));
    }
    [AllowAnonymous]
    // Get an order by id
    [HttpGet(ApiEndpoints.Orders.GetByOrderId)]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        return HandleResult(await Mediator.Send(new GetOrderByIdQuery { OrderId = id }));
    }
    [AllowAnonymous]
    // Create an order
    [HttpPost(ApiEndpoints.Orders.Create)]
    public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto, string userId)
    {
        return HandleResult(await Mediator.Send(new CreateOrderCommand {CreateOrderDto = createOrderDto, UserId = userId}));
    }
}