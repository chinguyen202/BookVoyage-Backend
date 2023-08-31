// using System;
// using System.Linq.Expressions;
// using System.Threading;
// using System.Threading.Tasks;
// using AutoMapper;
// using BookVoyage.Application.Common.Interfaces;
// using BookVoyage.Application.ShoppingCarts.Commands;
// using BookVoyage.Domain.Entities;
// using BookVoyage.Persistence.Data;
// using Microsoft.EntityFrameworkCore;
// using Moq;
// using YourNamespace; // Replace with your actual namespace
// using Xunit;
//
// public class UpsertShoppingCartCommandHandlerTests
// {
//     [Fact]
//     public async Task Handle_BookDoesNotExist_ReturnsFailure()
//     {
//         // Arrange
//         var dbContextMock = new Mock<IApplicationDbContext>();
//         var mapperMock = new Mock<IMapper>();
//         var handler = new UpsertShoppingCartCommandHandler(dbContextMock.Object, mapperMock.Object);
//
//         dbContextMock.Setup(db => db.Books.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()))
//             .ReturnsAsync((Book)null);
//
//         // Act
//
//         // Assert
//         Assert.False(result.IsSuccess);
//         Assert.Equal("Book does not exist", result.ErrorMessage);
//     }
//
//     [Fact]
//     public async Task Handle_NewCartAndValidQuantity_CreatesNewCartAndCartItem()
//     {
//         // Arrange
//         var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
//             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//             .Options;
//         
//         var dbContextMock = new Mock<IApplicationDbContext>();
//         dbContextMock.SetupAllProperties(); // Enable properties like ShoppingCarts and CartItems to be mocked
//         dbContextMock.Setup(db => db.SaveChangesAsync(CancellationToken.None))
//             .ReturnsAsync(1); // Simulate a successful save operation
//
//         var mapperMock = new Mock<IMapper>();
//         var handler = new UpsertShoppingCartCommandHandler(dbContextMock.Object, mapperMock.Object);
//         var command = new UpsertShoppingCartCommand(new UpsertShoppingCartDto
//         {
//             BuyerId = "9071E3D0-BF48-4859-86B7-F7B6AF6303BD",
//             BookId = Guid.NewGuid()
//             Quantity = 2
//         });
//
//         dbContextMock.Setup(db => db.Books.FirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()))
//             .ReturnsAsync(new Book());
//
//         // Act
//         var result = await handler.Handle(command, CancellationToken.None);
//
//         // Assert
//         Assert.True(result.IsSuccess);
//         // Add more assertions based on your logic
//     }
//
//     // Add more test cases as needed...
// }
