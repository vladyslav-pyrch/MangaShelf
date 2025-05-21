using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace MangaShelf.Catalogue.Api.IntegrationTests.Mangas;

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
        var createManga = new
        {
            Name = name,
            OwnerId = "any-nonempty-string"
        };

        //When
        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", createManga);
        testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());

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
        var manga = new
        {
            Name = "",
            OwnerId = "any-nonempty-string"
        };
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
        var manga = new
        {
            Name = string.Empty,
            OwnerId = "any-nonempty-string"
        };

        //When
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", manga);

        //Then
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GivenNameIsLongerThan50Characters_WhenCreatingManga()
    {
        var manga = new
        {
            Name = new string('a', 51),
            OwnerId = "any-nonempty-string"
        };

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", manga);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GivenOwnerIdIsEmpty_WhenCreatingManga()
    {
        var manga = new
        {
            Name = "Title",
            OwnerId = string.Empty
        };

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", manga);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GivenOwnerIdIsNull_WhenCreatingManga()
    {
        var manga = new
        {
            Name = "Title",
            OwnerId = string.Empty
        };
        manga = manga with { OwnerId = null };

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", manga);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GivenMangaWasCreated_WhenRetrievingById()
    {
        //Given
        const string mangaName = "Some manga title";
        var manga = new
        {
            Name = mangaName,
            OwnerId = "any-nonempty-string"
        };

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