using BookVoyage.Application.Authors.Commands;
using BookVoyage.Application.Common.Interfaces;
using BookVoyage.Domain.Entities;
using Moq;

namespace BookVoyage.Tests.Unit.Authors.Commands
{
    public class DeleteAuthorCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidAuthorId_ShouldReturnSuccess()
        {
            // Arrange
            var dbContextMock = new Mock<IApplicationDbContext>();
            var authorIdToDelete = Guid.NewGuid();
            
            var authorToDelete = new Author
            {
                Id = authorIdToDelete,
                FullName = "J.K. Rowling",
                Publisher = "Bloomsbury"
            };
            
            dbContextMock.Setup(db => db.Authors.FindAsync(authorIdToDelete))
                .ReturnsAsync(authorToDelete);
            
            dbContextMock.Setup(db => db.SaveChangesAsync(CancellationToken.None))
                .ReturnsAsync(1); 
            
            var handler = new DeleteAuthorCommandHandler(dbContextMock.Object);
            var command = new DeleteAuthorCommand { Id = authorIdToDelete };
            
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            
            // Assert
            Assert.True(result.IsSuccess);
        }
        
        [Fact]
        public async Task Handle_InvalidAuthorId_ShouldReturnFailure()
        {
            // Arrange
            var dbContextMock = new Mock<IApplicationDbContext>();
            var nonExistentAuthorId = Guid.NewGuid();
            
            dbContextMock.Setup(db => db.Authors.FindAsync(nonExistentAuthorId))
                .ReturnsAsync((Author)null); 
            
            var handler = new DeleteAuthorCommandHandler(dbContextMock.Object);
            var command = new DeleteAuthorCommand { Id = nonExistentAuthorId };
            
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Author can't be found!", result.Error);
        }
        
        [Fact]
        public async Task Handle_DatabaseError_ShouldReturnFailure()
        {
            // Arrange
            var dbContextMock = new Mock<IApplicationDbContext>();
            var authorIdToDelete = Guid.NewGuid();
            
            var authorToDelete = new Author
            {
                Id = authorIdToDelete,
                FullName = "J.K. Rowling",
                Publisher = "Bloomsbury"
            };
            
            dbContextMock.Setup(db => db.Authors.FindAsync(authorIdToDelete))
                .ReturnsAsync(authorToDelete);
            
            dbContextMock.Setup(db => db.SaveChangesAsync(CancellationToken.None))
                .ReturnsAsync(0); 
            
            var handler = new DeleteAuthorCommandHandler(dbContextMock.Object);
            var command = new DeleteAuthorCommand { Id = authorIdToDelete };
            
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Fail to delete the author", result.Error);
        }
    }
}
