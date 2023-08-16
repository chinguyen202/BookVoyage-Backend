using BookVoyage.Domain.Entities;

namespace BookVoyage.Persistence.Data;

public class Seed
{
    public static async Task SeedData(ApplicationDbContext context)
    {
        if (context.Categories.Any()) return;
        var categories = new List<Category>
        {
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Non-fiction",
                CreatedAt = DateTime.UtcNow.AddMonths(-2),
                ModifiedAt = DateTime.UtcNow.AddMonths(-2),
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Fiction",
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
                ModifiedAt = DateTime.UtcNow.AddMonths(-1),
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Biography",
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
                ModifiedAt = DateTime.UtcNow.AddMonths(-1),
            },
        };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }
}