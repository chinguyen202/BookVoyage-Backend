using AutoMapper;
using BookVoyage.Application.Authors;
using BookVoyage.Application.Authors.Commands;
using BookVoyage.Application.Common.Interfaces;
using BookVoyage.Domain.Entities;
using FluentValidation;
using Moq;

namespace BookVoyage.Tests.Unit.Authors.Commands;

public class CreateAuthorCommandTests
{
    [Fact]
    public async Task Handle_ValidRequest_ShouldCreateAuthor()
    {
        // Arrange
        var dbContextMock = new Mock<IApplicationDbContext>();
        var mapperMock = new Mock<IMapper>();
        var validatorMock = new Mock<IValidator<CreateAuthorCommand>>();
        var request = new CreateAuthorCommand()
        {
            AuthorDto = new AuthorDto()
            {
                FullName = "Anne Frank",
                Publisher = "Contact Publishing"
            }
        };
        var handler = new CreateAuthorCommandHandler(
            dbContextMock.Object,
            mapperMock.Object,
            validatorMock.Object
        );

        mapperMock.Setup(m => m.Map<Author>(It.IsAny<AuthorDto>()))
            .Returns(new Author());

        dbContextMock.Setup(d => d.Authors.Add(It.IsAny<Author>()));
        dbContextMock.Setup(d => d.SaveChangesAsync(CancellationToken.None))
            .ReturnsAsync(1);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    

    [Fact]
    public async Task Handle_DatabaseError_ShouldReturnFailureResult()
    {
        // Arrange
        var dbContextMock = new Mock<IApplicationDbContext>();
        var mapperMock = new Mock<IMapper>();
        var validatorMock = new Mock<IValidator<CreateAuthorCommand>>();

        var request = new CreateAuthorCommand
        {
            AuthorDto = new AuthorDto
            {
                FullName = "Anne Frank",
                Publisher = "Contact Publishing"
            }
        };

        var handler = new CreateAuthorCommandHandler(
            dbContextMock.Object,
            mapperMock.Object,
            validatorMock.Object
        );

        mapperMock.Setup(m => m.Map<Author>(It.IsAny<AuthorDto>()))
            .Returns(new Author());

        dbContextMock.Setup(d => d.Authors.Add(It.IsAny<Author>()));
        dbContextMock.Setup(d => d.SaveChangesAsync(CancellationToken.None))
            .ReturnsAsync(0); // Simulate a database error

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Failed to create Author", result.Error);
    }

    [Fact]
    public async Task Handle_MappingError_ShouldReturnFailureResult()
    {
        // Arrange
        var dbContextMock = new Mock<IApplicationDbContext>();
        var mapperMock = new Mock<IMapper>();
        var validatorMock = new Mock<IValidator<CreateAuthorCommand>>();

        var request = new CreateAuthorCommand
        {
            AuthorDto = new AuthorDto
            {
                // Initialize AuthorDto properties here
            }
        };

        var handler = new CreateAuthorCommandHandler(
            dbContextMock.Object,
            mapperMock.Object,
            validatorMock.Object
        );

        mapperMock.Setup(m => m.Map<Author>(It.IsAny<AuthorDto>()))
            .Throws(new AutoMapperMappingException("Mapping error")); // Simulate mapping error

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Mapping error", result.Error);
    }
}