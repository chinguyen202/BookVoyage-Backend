using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BookVoyage.Domain.Entities;

namespace BookVoyage.Persistence.Data;

public class ApplicationDbContext: DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions options, IConfiguration configuration): base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    
    static ApplicationDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new TimeStampInterceptor());
        optionsBuilder.UseNpgsql().UseSnakeCaseNamingConvention();
    }
    
} 