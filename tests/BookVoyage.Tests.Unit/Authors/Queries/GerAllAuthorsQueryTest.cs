using AutoMapper;
using BookVoyage.Application.Authors.Queries;
using BookVoyage.Application.Common.Mappings;
using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace BookVoyage.Tests.Unit.Authors.Queries
{
    public class GetAllAuthorsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidQuery_ShouldReturnListOfAuthors()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var configuration = new ConfigurationBuilder().Build(); // You can configure this as needed

            var dbContext = new ApplicationDbContext(options, configuration);

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var authors = new List<Author>
            {
                new Author { Id = Guid.NewGuid(), FullName = "Author 1", Publisher = "Publisher 1" },
                new Author { Id = Guid.NewGuid(), FullName = "Author 2", Publisher = "Publisher 2" }
            };

            dbContext.Authors.AddRange(authors);
            dbContext.SaveChanges();

            var query = new GetAllAuthorsQuery();
            var handler = new GetAllAuthorsQueryHandler(dbContext, mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(authors.Count, result.Value.Count);

            for (var i = 0; i < authors.Count; i++)
            {
                Assert.Equal(authors[i].Id, result.Value[i].Id);
                Assert.Equal(authors[i].FullName, result.Value[i].FullName);
                Assert.Equal(authors[i].Publisher, result.Value[i].Publisher);
            }
        }
        
        [Fact]
        public async Task Handle_EmptyDatabase_ShouldReturnEmptyList()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var configuration = new ConfigurationBuilder().Build(); // You can configure this as needed

            var dbContext = new ApplicationDbContext(options, configuration);

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var query = new GetAllAuthorsQuery();
            var handler = new GetAllAuthorsQueryHandler(dbContext, mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }
        
    }
}