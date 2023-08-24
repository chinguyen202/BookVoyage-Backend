using BookVoyage.Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using BookVoyage.Application.Orders.Commands;
using BookVoyage.Application.Orders.Queries;
using BookVoyage.Utility.Constants;

namespace BookVoyage.WebApi.Controllers;

/// <summary>
/// Order endpoint
/// </summary>
public class OrdersController: BaseApiController
{
    // Get all orders of user
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.V1.Orders.GetByUserId)]
    public async Task<IActionResult> GetOrders(string? id)
    {
        return HandleResult(await Mediator.Send(new GetOrdersQuery {UserId = id}));
    }

    // Get an order by id
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.V1.Orders.GetByOrderId)]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        return HandleResult(await Mediator.Send(new GetOrderByIdQuery { OrderId = id }));
    }
    
    // Create an order
    [AllowAnonymous]
    [HttpPost(ApiEndpoints.V1.Orders.Create)]
    public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto, string userId)
    {
        return HandleResult(await Mediator.Send(new CreateOrderCommand {CreateOrderDto = createOrderDto, UserId = userId}));
    }
    
    // Update an order
    [AllowAnonymous]
    [HttpPut(ApiEndpoints.V1.Orders.UpdateStatus)]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderUpdatedDto orderUpdatedDto)
    {
        return HandleResult(await Mediator.Send(new UpdateOrderCommand { OrderUpdatedDto = orderUpdatedDto }));
    }
}