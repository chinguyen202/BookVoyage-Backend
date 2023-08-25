using BookVoyage.Domain.Entities;
using BookVoyage.Domain.Entities.OrderAggegate;
using BookVoyage.Domain.Entities.UserAggegate;
using Microsoft.EntityFrameworkCore;

namespace BookVoyage.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}