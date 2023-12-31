using BookVoyage.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using BookVoyage.Domain.Entities;
using BookVoyage.Domain.Entities.OrderAggegate;
using BookVoyage.Domain.Entities.UserAggegate;
using BookVoyage.Utility.Constants;

namespace BookVoyage.Persistence.Data;

/// <summary>
/// 
/// </summary>
public class ApplicationDbContext:  IdentityDbContext<ApplicationUser>,IApplicationDbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration): base(options)
    {
        _configuration = configuration;
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    static ApplicationDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new TimeStampInterceptor());
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating((builder));
        // Configuration

        builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole {Name = SD.Admin, NormalizedName = "ADMIN"},
                new IdentityRole {Name = SD.Customer, NormalizedName = "CUSTOMER"}
            );

        builder.Entity<ApplicationUser>()
            .HasOne(u => u.Address)
            .WithOne()
            .HasForeignKey<UserAddress>(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
} 