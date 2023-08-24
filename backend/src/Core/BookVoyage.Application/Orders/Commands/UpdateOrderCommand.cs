using AutoMapper;
using BookVoyage.Application.Common;
using BookVoyage.Persistence.Data;
using MediatR;

namespace BookVoyage.Application.Orders.Commands;

public record UpdateOrderCommand: IRequest<ApiResult<Unit>>
{
    public OrderUpdatedDto OrderUpdatedDto { get; set; }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ApiResult<Unit>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<Unit>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderInDb = _context.Orders.FirstOrDefault(u => u.Id == request.OrderUpdatedDto.OrderId);
        if (orderInDb == null)
        {
            return ApiResult<Unit>.Failure("The order does not exist");
        }

        orderInDb.OrderStatus = request.OrderUpdatedDto.OrderStatus;
        var result = await _context.SaveChangesAsync() > 0;
        if (!result)
        {
            return ApiResult<Unit>.Failure("Error in update the order");
        }

        return ApiResult<Unit>.Success(Unit.Value);
    }
}