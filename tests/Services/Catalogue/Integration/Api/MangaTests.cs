using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace MangaShelf.Catalogue.Api.IntegrationTests;

public class MangaTests(WebApplicationFactory<Program> webApplicationFactory, ITestOutputHelper testOutputHelper)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient = webApplicationFactory.CreateClient();

    [Theory]
    [InlineData("Naruto")]
    [InlineData("Bleach")]
    [InlineData("One Piece")]
    public async Task GivenName_WhenCreatingManga(string name)
    {
        var manga = new { Name = name };

        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", manga);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Uri? location = response.Headers.Location;
        location.Should().NotBeNull();
        location.ToString().Should().MatchRegex("^/manga/[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}$");

        var mangaId = await response.Content.ReadFromJsonAsync<Guid>();
        mangaId.Should().NotBeEmpty();

        testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GivenMangaWasCreated_WhenRetrievingById()
    {
        const string mangaName = "Some manga title";
        var manga = new { Name = mangaName };

        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", manga);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Uri? location = response.Headers.Location;
        location.Should().NotBeNull();

        var mangaRetrieved = await _httpClient.GetFromJsonAsync<MangaDto>(location);
        mangaRetrieved.Should().NotBeNull();
        mangaRetrieved.Id.Should().NotBeEmpty();
        mangaRetrieved.Name.Should().BeEquivalentTo(mangaName);
    }

    private class MangaDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}