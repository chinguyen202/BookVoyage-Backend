using System.Net;
using Bogus;
using BookVoyage.Application.Authors;
using FluentAssertions;
using Xunit;

namespace BookVoyage.Tests.Integration.Authors;

public class DeleteAuthorControllerTests: IClassFixture<BookVoyageApiFactory>
{
    private readonly HttpClient _client;
    

    public DeleteAuthorControllerTests(BookVoyageApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenAuthorDoesNotExists()
    {
        // Act
        var response = await _client.DeleteAsync($"v1/api/v1/authors/{Guid.NewGuid()}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        
    }

    [Fact(Skip = "User does not exist anymore")]
    public async Task Delete_ReturnsOk_WhenAuthorsExist()
    {
        // Act
        var response = await _client.DeleteAsync("api/v1/authors/26e29fec-23b9-4ee1-9349-6a1c7da353c8");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    
    }
}