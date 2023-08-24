using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Persistence.Data;


namespace BookVoyage.Application.Orders.Queries;

/// <summary>
/// Represent a query to get all orders with associated order items for a specific user.
/// </summary>
public record GetOrdersQuery: IRequest<ApiResult<List<OrderDto>>>
{
    public string? UserId { get; set; }
}

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery,ApiResult<List<OrderDto>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOrdersQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResult<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var ordersInDb =  _dbContext.Orders
            .Include(u => u.OrderItems)
            .ThenInclude(u => u.BookOrderedItem)
            .OrderByDescending(u => u.Id);
        if (string.IsNullOrEmpty(request.UserId))
        {
            return ApiResult<List<OrderDto>>.Failure("Failed to get users' information");
        }
        var orders = await ordersInDb
            .Where(u => u.BuyerId == request.UserId)
            .ToListAsync();

        var orderDtos = _mapper.Map<List<OrderDto>>(orders);

        return ApiResult<List<OrderDto>>.Success(orderDtos);
    }
}