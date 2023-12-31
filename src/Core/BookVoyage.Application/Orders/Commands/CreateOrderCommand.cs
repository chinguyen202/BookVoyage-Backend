using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using BookVoyage.Application.Common;
using BookVoyage.Application.Common.Interfaces;
using BookVoyage.Domain.Entities.OrderAggegate;
using BookVoyage.Domain.Entities.UserAggegate;

namespace BookVoyage.Application.Orders.Commands;

public record CreateOrderCommand : IRequest<ApiResult<Unit>>
{
    public CreateOrderDto CreateOrderDto { get; set; }
    public string UserId { get; set; }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<Unit>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResult<Unit>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Get the shopping cart of user
        var shoppingCart = await _dbContext.ShoppingCarts
            .Include(u => u.CartItems)
            .ThenInclude(u => u.Book)
            .FirstOrDefaultAsync(u => u.BuyerId == request.UserId, cancellationToken: cancellationToken);
        if (shoppingCart == null)
        {
            return ApiResult<Unit>.Failure("User doesn't have any shopping cart");
        }
        // Convert the shopping cart item to order item
        var items = new List<OrderItem>();
        foreach (var item in shoppingCart.CartItems)
        {
            var bookItem = await _dbContext.Books.FindAsync(item.BookId);
            var itemOrdered = new BookOrderedItem
            {
                BookId = bookItem.Id,
                BookName = bookItem.Title,
                ImageUrl = bookItem.ImageUrl
            };
            var orderItem = new OrderItem
            {
                Price = bookItem.UnitPrice,
                Quantity = item.Quantity,
                BookOrderedItem = itemOrdered,
            };
            items.Add(orderItem);
            bookItem.UnitInStock -= item.Quantity;
        }

        // Calculate the sub total
        var subtotal = items.Sum(item => item.Quantity * item.Price);
        var totalQuantity = items.Sum(item => item.Quantity);
        // Create new order
        var order = new Order
        {
            OrderItems = items,
            BuyerId = request.UserId,
            ShippingAddress = request.CreateOrderDto.ShippingAddress,
            Subtotal = subtotal,
            TotalQuantity = totalQuantity,
            OrderStatus = OrderStatus.Pending,
            StripePaymentIntentId = "fdjashkkfj"
        };
        // Save the order to database and remove the shoppping cart from database
        _dbContext.Orders.Add(order);
        _dbContext.ShoppingCarts.Remove(shoppingCart);
        // If user want to save shipping address, update the user's address
        if (request.CreateOrderDto.SaveAddress)
        {
            var user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == request.UserId, cancellationToken: cancellationToken);
            if (user != null)
            {
                user.Address = new UserAddress
                {
                    FullName = request.CreateOrderDto.ShippingAddress.FullName,
                    Street = request.CreateOrderDto.ShippingAddress.Street,
                    PostCode = request.CreateOrderDto.ShippingAddress.PostCode,
                    State = request.CreateOrderDto.ShippingAddress.State,
                    Country = request.CreateOrderDto.ShippingAddress.Country
                };
                _dbContext.ApplicationUsers.Update(user);
            }
        }
        // Check the result and return a value
        var result = await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        if (!result)
        {
            return ApiResult<Unit>.Failure("Error when trying to create an order");
        }
        return ApiResult<Unit>.Success(Unit.Value);
    }
}
    

