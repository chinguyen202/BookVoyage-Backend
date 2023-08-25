using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;

namespace BookVoyage.Application.Orders.Queries;

/// <summary>
/// Represents a query and its handler to retrieve an order by its ID.
/// </summary>
public record GetOrderByIdQuery: IRequest<ApiResult<OrderDto>>
{
    public Guid OrderId { get; init; }
}

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ApiResult<OrderDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<ApiResult<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.OrderId == Guid.Empty)
        {
            return ApiResult<OrderDto>.Failure("Failed to get the order");
        }

        var orderInDb = await _dbContext.Orders
            .Include(x => x.OrderItems)
            .Where(u => u.Id == request.OrderId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (orderInDb == null)
        {
            return ApiResult<OrderDto>.Failure("Order not found");
        }

        var orderFound = _mapper.Map<OrderDto>(orderInDb);
        return ApiResult<OrderDto>.Success(orderFound);
    }
}