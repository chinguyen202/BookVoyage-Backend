using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using BookVoyage.Domain.Entities;
using Npgsql;

namespace BookVoyage.Persistence.Data;

public class ApplicationDbContext: IdentityDbContext<AppUser>
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions options, IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Category> Categories { get; set; }
    // public DbSet<Book> Books { get; set; }
    // public DbSet<Author> Authors { get; set; }
    
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
    
} 