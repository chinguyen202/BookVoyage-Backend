using AutoMapper;
using MediatR;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;
using BookVoyage.Domain.Entities.OrderAggegate;

namespace BookVoyage.Application.Orders.Commands;

public record UpdateOrderCommand: IRequest<ApiResult<Unit>>
{
    public OrderUpdatedDto OrderUpdatedDto { get; set; }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ApiResult<Unit>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<Unit>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        // Check the order id
        var orderInDb = _context.Orders.FirstOrDefault(u => u.Id == request.OrderUpdatedDto.OrderId);
        if (orderInDb == null)
        {
            return ApiResult<Unit>.Failure("The order does not exist");
        }

        var orderUpdate = _mapper.Map<Order>(request.OrderUpdatedDto);
        orderInDb.OrderStatus = orderUpdate.OrderStatus;
        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
        if (!result)
        {
            return ApiResult<Unit>.Failure("Error in update the order");
        }

        return ApiResult<Unit>.Success(Unit.Value);
    }
}