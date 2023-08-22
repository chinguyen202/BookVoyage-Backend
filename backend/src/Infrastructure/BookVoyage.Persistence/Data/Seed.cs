using BookVoyage.Domain.Entities;
using BookVoyage.Domain.Entities.UserAggegate;
using BookVoyage.Utility.Constants;
using Microsoft.AspNetCore.Identity;

namespace BookVoyage.Persistence.Data;

public class Seed
{
    public static async Task SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { FirstName = "Bon", LastName = "Smith", UserName = "bob", Email = "bob@test.com" },
                new ApplicationUser { FirstName = "Tom", LastName = "Smith", UserName = "tom", Email = "tom@test.com" },
                new ApplicationUser { FirstName = "Ree", LastName = "Smith", UserName = "tep", Email = "tep@test.com" },
            };
            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
        
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
        
        if(context.Authors.Any()) return;
        var authors = new List<Author>
        {
            new Author
            {
                Id = Guid.NewGuid(),
                FullName = "Agatha Christie",
                Publisher = "HarperCollins",
                CreatedAt = DateTime.UtcNow.AddMonths(-2),
                ModifiedAt = DateTime.UtcNow.AddMonths(-2),
            },
            new Author
            {
                Id = Guid.NewGuid(),
                FullName = "Patrick Radden Keefe",
                Publisher = "Penguin Random House",
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
                ModifiedAt = DateTime.UtcNow.AddMonths(-1),
            },
            new Author
            {
                Id = Guid.NewGuid(),
                FullName = "Walter Isaacson",
                Publisher = "Simon & Schuster",
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
                ModifiedAt = DateTime.UtcNow.AddMonths(-1),
            }
        };
        await context.Authors.AddRangeAsync(authors);
        await context.SaveChangesAsync();
    }
}