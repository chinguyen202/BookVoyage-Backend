using System.Security.Claims;
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
    [HttpGet(ApiEndpoints.V1.Orders.GetByUserId)]
    public async Task<IActionResult> GetOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return HandleResult(await Mediator.Send(new GetOrdersQuery {UserId = userId}));
    }

    // Get an order by id
    [AllowAnonymous]
    [HttpGet(ApiEndpoints.V1.Orders.GetByOrderId)]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        return HandleResult(await Mediator.Send(new GetOrderByIdQuery { OrderId = id }));
    }
    
    // Create an order
    [Authorize(Roles = SD.Admin)]
    [HttpPost(ApiEndpoints.V1.Orders.Create)]
    public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return HandleResult(await Mediator.Send(new CreateOrderCommand {CreateOrderDto = createOrderDto, UserId = userId}));
    }
    
    // Update an order
    [Authorize(Roles = SD.Admin)]
    [HttpPut(ApiEndpoints.V1.Orders.UpdateStatus)]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderUpdatedDto orderUpdatedDto)
    {
        return HandleResult(await Mediator.Send(new UpdateOrderCommand { OrderUpdatedDto = orderUpdatedDto }));
    }
    
    // Get all orders in the database
    // For Admin only
    [Authorize(Roles = SD.Admin)]
    [HttpGet(ApiEndpoints.V1.Orders.GetAll)]
    public async Task<IActionResult> GetAllOrders()
    {
        return HandleResult(await Mediator.Send(new GetAllOrderQuery()));
    }
}