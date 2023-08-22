using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Application.ShoppingCarts.Queries;

public record GetCartQuery: IRequest<ApiResult<ShoppingCart>>
{
    public string Id { get; set; }
}

public class GetCartQueryHandler: IRequestHandler<GetCartQuery, ApiResult<ShoppingCart>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetCartQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ApiResult<ShoppingCart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _dbContext.ShoppingCarts
            .Include(u => u.CartItems)
            .ThenInclude(u => u.Book)
            .FirstOrDefaultAsync(u => u.BuyerId == request.Id);
        if (shoppingCart == null)
        {
            return ApiResult<ShoppingCart>.Failure("User currently doesn't have any shoppping cart");
        }
        if (shoppingCart.CartItems != null && (shoppingCart.CartItems.Count > 0))
        {
            shoppingCart.CartTotal = shoppingCart.CartItems.Sum(u => u.Quantity * u.Book.UnitPrice);
        }
        return ApiResult<ShoppingCart>.Success(shoppingCart);
    }
}

