using Microsoft.EntityFrameworkCore;

using BookVoyage.Domain.Entities;

namespace BookVoyage.Persistence.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions options): base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
} 