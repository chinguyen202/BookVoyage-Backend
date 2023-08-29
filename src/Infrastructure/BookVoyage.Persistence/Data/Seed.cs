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
            if (!userManager.Users.Any())
            {
                var adminUser = new ApplicationUser
                {
                    FirstName = "Admin",
                    LastName = "User",
                    UserName = "admin",
                    Email = "admin@test.com",
                };
                await userManager.CreateAsync(adminUser, "Admin123!"); 
                
                // Assign the admin role to the admin user
                await userManager.AddToRoleAsync(adminUser, SD.Admin);
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