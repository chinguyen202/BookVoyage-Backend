using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;

namespace BookVoyage.Application.ShoppingCarts.Queries;

public record GetCartQuery: IRequest<ApiResult<ShoppingCartResponseDto>>
{
    public string Id { get; set; }
}

public class GetCartQueryHandler : IRequestHandler<GetCartQuery, ApiResult<ShoppingCartResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCartQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResult<ShoppingCartResponseDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Id))
        {
            return ApiResult<ShoppingCartResponseDto>.Failure("Can't found user");
        }
        var shoppingCart = await _dbContext.ShoppingCarts
            .Include(u => u.CartItems)
            .ThenInclude(u => u.Book)
            .FirstOrDefaultAsync(u => u.BuyerId == request.Id, cancellationToken: cancellationToken);
        if (shoppingCart != null)
        {
            var response = _mapper.Map<ShoppingCartResponseDto>(shoppingCart);
            return ApiResult<ShoppingCartResponseDto>.Success(response);
        }
        return ApiResult<ShoppingCartResponseDto>.Failure("The user doesn't have an existing shopping cart");
    }
    
}


