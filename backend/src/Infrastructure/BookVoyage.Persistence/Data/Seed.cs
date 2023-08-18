using BookVoyage.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookVoyage.Persistence.Data;

public class Seed
{
    public static async Task SeedData(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var users = new List<AppUser>
            {
                new AppUser { FirstName = "Bon", LastName = "Smith", UserName = "bob", Email = "bob@test.com" },
                new AppUser { FirstName = "Tom", LastName = "Smith", UserName = "tom", Email = "tom@test.com" },
                new AppUser { FirstName = "Ree", LastName = "Smith", UserName = "tep", Email = "tep@test.com" },
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
        //
        //
        // if (context.Authors.Any()) return;
        // var authors = new List<Author>
        // {
        //     new Author
        //     {
        //         Id = Guid.NewGuid(),
        //         FirstName = "John",
        //         LastName = "Smith",
        //         CreatedAt = DateTime.UtcNow.AddMonths(-2),
        //         ModifiedAt = DateTime.UtcNow.AddMonths(-2),
        //     },
        //     new Author
        //     {
        //         Id = Guid.NewGuid(),
        //         FirstName = "Madam",
        //         LastName = "Smith",
        //         CreatedAt = DateTime.UtcNow.AddMonths(-2),
        //         ModifiedAt = DateTime.UtcNow.AddMonths(-2),
        //     },
        //     new Author
        //     {
        //         Id = Guid.NewGuid(),
        //         FirstName = "Kate",
        //         LastName = "Smith",
        //         CreatedAt = DateTime.UtcNow.AddMonths(-2),
        //         ModifiedAt = DateTime.UtcNow.AddMonths(-2),
        //     },
        // };
        // await context.Authors.AddRangeAsync(authors);
        // await context.SaveChangesAsync();
        //
        // if (context.Books.Any()) return;
        // var books = new List<Book>
        // {
        //     new Book
        //     {
        //         Id = Guid.NewGuid(),
        //         Title = "Alice's Adventures in Wonderland & Other Stories",
        //         Summary = "Down the rabbit-hole and through the looking-glass! Alice's Adventures in Wonderland & Other Stories features all of the best-known works of Lewis Carroll, including the novels Alice's Adventures in Wonderland and Through the Looking-Glass, with the classic illustrations of John Tenniel. This compilation also features Carroll's novels Sylvie and Bruno and Sylvie and Bruno Concluded, his masterpiece of nonsense verse The Hunting of the Snark, and miscellaneous poems, short stories, puzzles, and acrostics.",
        //         UnitPrice = 25.00,
        //         ISBN = "9781435166240",
        //         YearOfPublished = 2018,
        //         UnitInStock = 100,
        //         Publisher = "Barnes & Noble",
        //         ImageUrl = "https://prodimage.images-bn.com/lf?set=key%5Bresolve.pixelRatio%5D,value%5B1%5D&set=key%5Bresolve.width%5D,value%5B600%5D&set=key%5Bresolve.height%5D,value%5B10000%5D&set=key%5Bresolve.imageFit%5D,value%5Bcontainerwidth%5D&set=key%5Bresolve.allowImageUpscaling%5D,value%5B0%5D&set=key%5Bresolve.format%5D,value%5Bwebp%5D&source=url%5Bhttps://prodimage.images-bn.com/pimages/9781435166240_p0_v1_s600x595.jpg%5D&scale=options%5Blimit%5D,size%5B600x10000%5D&sink=format%5Bwebp%5D",
        //         CreatedAt = DateTime.UtcNow.AddMonths(-2),
        //         ModifiedAt = DateTime.UtcNow.AddMonths(-2),
        //     }
        //
        // };
        // await context.Books.AddRangeAsync(books);
        // await context.SaveChangesAsync();
    }
}