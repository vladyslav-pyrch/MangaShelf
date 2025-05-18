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
        //Given
        var manga = new { Name = name };

        //When
        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", manga);

        //Then
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Uri? location = response.Headers.Location;
        var mangaId = await response.Content.ReadFromJsonAsync<Guid>();

        location.Should().NotBeNull();
        location.ToString().Should().MatchRegex("^/manga/[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}$");
        mangaId.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GivenNameIsNull_WhenCreatingManga()
    {
        //Given
        var manga = new { Name = "" };
        manga = manga with { Name = null };
        
        //When
        HttpResponseMessage request = await _httpClient.PostAsJsonAsync("create-manga", manga);
        
        //Then
        request.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        testOutputHelper.WriteLine(await request.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GivenNameIsEmpty_WhenCreatingManga()
    {
        //Given
        var manga = new { Name = string.Empty };

        //When
        HttpResponseMessage request = await _httpClient.PostAsJsonAsync("create-manga", manga);

        //Then
        request.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenMangaWasCreated_WhenRetrievingById()
    {
        //Given
        const string mangaName = "Some manga title";
        var manga = new { Name = mangaName };

        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", manga);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        Uri? location = response.Headers.Location;
        location.Should().NotBeNull();

        //When
        var mangaRetrieved = await _httpClient.GetFromJsonAsync<MangaDto>(location);

        //Then
        mangaRetrieved.Should().NotBeNull();
        mangaRetrieved.Id.Should().NotBeEmpty();
        mangaRetrieved.Name.Should().BeEquivalentTo(mangaName);
    }

    [Fact]
    public async Task GivenMangaDoesNotExist_WhenRetrievingById()
    {
        //Given
        Guid id = Guid.Parse("1b86f6ae-442a-4f49-acfc-90a173f514a8");
        var requestUri = $"/manga/{id}";

        //When
        using HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

        //Then
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private class MangaDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}