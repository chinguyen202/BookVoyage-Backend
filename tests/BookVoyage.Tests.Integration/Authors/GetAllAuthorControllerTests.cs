using System.Net;
using System.Net.Http.Json;
using Bogus;
using BookVoyage.Application.Authors;
using BookVoyage.Application.Authors.Queries;
using FluentAssertions;
using Xunit;

namespace BookVoyage.Tests.Integration.Authors;

public class GetAllAuthorControllerTests: IClassFixture<BookVoyageApiFactory>
{
    private readonly HttpClient _client;
    
    public GetAllAuthorControllerTests(BookVoyageApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ReturnsAllAuthors_WhenAuthorsExists()
    {
        // Act
        var response = await _client.GetAsync("api/v1/authors");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
}