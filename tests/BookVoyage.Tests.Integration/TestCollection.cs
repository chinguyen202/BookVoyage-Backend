using BookVoyage.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BookVoyage.Tests.Integration;

[CollectionDefinition("BookVoyageApi Collection")]
public class TestCollection: ICollectionFixture<WebApplicationFactory<IApiMarker>>
{
    
}