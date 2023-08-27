using AutoMapper;
using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Application.Orders.Queries;

/// <summary>
/// Get all order from database
/// </summary>
public record GetAllOrderQuery : IRequest<ApiResult<List<OrderDto>>>;

public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, ApiResult<List<OrderDto>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllOrderQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResult<List<OrderDto>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        var ordersInDb =  _dbContext.Orders
            .Include(u => u.OrderItems)
            .ThenInclude(u => u.BookOrderedItem)
            .OrderByDescending(u => u.Id);

        var orderDtos = _mapper.Map<List<OrderDto>>(ordersInDb);

        return ApiResult<List<OrderDto>>.Success(orderDtos);
    }
}