using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;
using BookVoyage.Domain.Entities;

namespace BookVoyage.Application.ShoppingCarts.Commands;

/// <summary>
/// Insert/updating request for shopping cart
/// </summary>
public record UpsertShoppingCartCommand : IRequest<ApiResult<Unit>>
{
    public UpsertShoppingCartDto UpsertShoppingCartDto { get; set; }
}

/// <summary>
/// Insert/update shopping cart handler
/// </summary>
public class UpsertShoppingCartCommandHandler : IRequestHandler<UpsertShoppingCartCommand, ApiResult<Unit>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpsertShoppingCartCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<ApiResult<Unit>> Handle(UpsertShoppingCartCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the shopping cart
        ShoppingCart shoppingCart =
            _dbContext.ShoppingCarts.Include(u => u.CartItems).FirstOrDefault(u => u.BuyerId == request.UpsertShoppingCartDto.BuyerId);
        // Retrieve the book
        Book book = _dbContext.Books.FirstOrDefault(u => u.Id == request.UpsertShoppingCartDto.BookId);
        if (book == null)
        {
            return ApiResult<Unit>.Failure("Book does not exist");
        }

        if (shoppingCart == null && request.UpsertShoppingCartDto.Quantity > 0)
        {
            // Create a shopping cart
            ShoppingCart newCart = new()
            {
                BuyerId = request.UpsertShoppingCartDto.BuyerId,
                CartItems = null
            };
            _dbContext.ShoppingCarts.Add(newCart);
            var result = await _dbContext.SaveChangesAsync(cancellationToken) > 0;
            if (!result) return ApiResult<Unit>.Failure("Fail to add shopping cart");
            // Create a new cart item
            CartItem newCartItem = new()
            {
                BookId = request.UpsertShoppingCartDto.BookId,
                Quantity = request.UpsertShoppingCartDto.Quantity,
            };
            _dbContext.CartItems.Add(newCartItem);
            var resultCartItem = await _dbContext.SaveChangesAsync(cancellationToken) > 0;
            if (!resultCartItem) return ApiResult<Unit>.Failure("Fail to add cart item");
        }
        else
        {
            // Shopping cart already exist 
            CartItem cartItemInCart =
                shoppingCart.CartItems.FirstOrDefault(u => u.BookId == request.UpsertShoppingCartDto.BookId);
            if (cartItemInCart == null)
            {
                // The item does not exist in the current cart
                CartItem cartItem = new()
                {
                    Quantity = request.UpsertShoppingCartDto.Quantity,
                    ShoppingCartId = shoppingCart.Id,
                    BookId = request.UpsertShoppingCartDto.BookId
                };
                _dbContext.CartItems.Add(cartItem);
                var result = await _dbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                // The item already exist in the shopping cart => update quantity
                int newQuantity = cartItemInCart.Quantity + request.UpsertShoppingCartDto.Quantity;
                if (request.UpsertShoppingCartDto.Quantity == 0 || request.UpsertShoppingCartDto.Quantity <= 0)
                {
                    // remove cart item from cart
                    // if it is the only item in shopping cart, remove the shopping cart
                    _dbContext.CartItems.Remove(cartItemInCart);
                    if (shoppingCart.CartItems.Count() == 1)
                    {
                        _dbContext.ShoppingCarts.Remove(shoppingCart);
                    }

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    cartItemInCart.Quantity = newQuantity;
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
            }
        }
        return ApiResult<Unit>.Success(Unit.Value);
    }
}

