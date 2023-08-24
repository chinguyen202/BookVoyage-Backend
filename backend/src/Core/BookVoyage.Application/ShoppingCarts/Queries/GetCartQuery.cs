using MediatR;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;

namespace BookVoyage.Application.ShoppingCarts.Queries;

public record GetCartQuery: IRequest<ApiResult<ShoppingCart>>
{
    public string Id { get; set; }
}

public class GetCartQueryHandler : IRequestHandler<GetCartQuery, ApiResult<ShoppingCart>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetCartQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResult<ShoppingCart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Id))
        {
            return ApiResult<ShoppingCart>.Failure("Can't found user");
        }
        var shoppingCart = await _dbContext.ShoppingCarts
            .Include(u => u.CartItems)
            .ThenInclude(u => u.Book)
            .FirstOrDefaultAsync(u => u.BuyerId == request.Id);
        if (shoppingCart == null)
        {
            return ApiResult<ShoppingCart>.Failure("The user doesn't have an existing shopping cart");
        }
        return ApiResult<ShoppingCart>.Success(shoppingCart);
    }
    
}


