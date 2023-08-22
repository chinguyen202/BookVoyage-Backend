using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities.OrderAggegate;
using BookVoyage.Domain.Entities.UserAggegate;
using BookVoyage.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Application.Orders.Queries;

public record GetOrdersQuery: IRequest<ApiResult<List<Order>>>
{
    public string? UserId { get; set; }
}

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery,ApiResult<List<Order>>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetOrdersQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResult<List<Order>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var ordersInDb =  _dbContext.Orders
            .Include(u => u.OrderItems)
            .ThenInclude(u => u.BookOrderedItem)
            .OrderByDescending(u => u.Id);
        if (string.IsNullOrEmpty(request.UserId))
        {
            return ApiResult<List<Order>>.Failure("Failed to get orders of user");
        }
        return ApiResult<List<Order>>.Success(await ordersInDb
            .Where(u => u.BuyerId == request.UserId)
            .ToListAsync()
        );
    }
}