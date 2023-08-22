using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities.OrderAggegate;
using BookVoyage.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Application.Orders.Queries;

public record GetOrderByIdQuery: IRequest<ApiResult<Order>>
{
    public Guid OrderId { get; set; }
}

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ApiResult<Order>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetOrderByIdQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ApiResult<Order>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.OrderId == Guid.Empty)
            return ApiResult<Order>.Failure("Failed to get the order");
        return ApiResult<Order>.Success(await _dbContext.Orders
            .Include(x => x.OrderItems)
            .Where(u => u.Id == request.OrderId)
            .FirstOrDefaultAsync());
    }
}