using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Npgsql;

using BookVoyage.Domain.Entities;

namespace BookVoyage.Persistence.Data;

/// <summary>
/// 
/// </summary>
public class ApplicationDbContext: IdentityDbContext<AppUser>
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions options, IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    
    static ApplicationDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new NpgsqlDataSourceBuilder(_configuration.GetConnectionString("DefaultConnection"));
        optionsBuilder.AddInterceptors(new TimeStampInterceptor());
        optionsBuilder.UseNpgsql(builder.Build()).UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating((builder));
        // Configuration
        
        builder.Entity<Book>(entity =>
        {
            entity.HasMany(a => a.Authors)
                .WithMany(p => p.Books)
                .UsingEntity(
                    l => l.HasOne(typeof(Book)).WithMany().OnDelete(DeleteBehavior.NoAction),
                    r => r.HasOne(typeof(Author)).WithMany().OnDelete(DeleteBehavior.NoAction));
        });
    }
} 