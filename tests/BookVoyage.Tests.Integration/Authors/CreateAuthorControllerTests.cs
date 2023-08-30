using System.Net;
using System.Net.Http.Json;
using Bogus;
using BookVoyage.Application.Authors;
using FluentAssertions;
using Xunit;

namespace BookVoyage.Tests.Integration.Authors;

public class CreateAuthorControllerTests: IClassFixture<BookVoyageApiFactory>
{
    private readonly HttpClient _client;
    
    private readonly Faker<AuthorDto> _authorGenerator = new Faker<AuthorDto>()
        .RuleFor(a => a.FullName, f => f.Name.FullName())
        .RuleFor(a => a.Publisher, f => f.Company.CompanyName());

    public CreateAuthorControllerTests(BookVoyageApiFactory bookVoyageApiFactory)
    {
        _client = bookVoyageApiFactory.CreateClient();
    }

    [Fact]
    public async Task Create_CreatesAuthor_WhenDataIsValid()
    {
        // Arrange
        var author = _authorGenerator.Generate();
        
        // Act
        var response = await _client.PostAsJsonAsync("api/v1/authors", author);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().Be("{}");
    }
    
    [Fact]
    public async Task Create_ReturnsBadRequest_WhenDataIsInvalid()
    {
        // Arrange
        var invalidAuthor = new AuthorDto(); 

        // Act
        var response = await _client.PostAsJsonAsync("api/v1/authors", invalidAuthor);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
    
}