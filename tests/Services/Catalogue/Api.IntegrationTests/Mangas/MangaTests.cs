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
            AuthorId = "any-nonempty-string"
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
            AuthorId = "any-nonempty-string"
        };
        manga = manga with { Name = null };
        
        //When
        using HttpResponseMessage request = await _httpClient.PostAsJsonAsync("create-manga", manga);
        
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
            AuthorId = "any-nonempty-string"
        };

        //When
        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", manga);

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
            AuthorId = "any-nonempty-string"
        };

        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", manga);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GivenAuthorIdIsEmpty_WhenCreatingManga()
    {
        var manga = new
        {
            Name = "Title",
            AuthorId = string.Empty
        };

        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", manga);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GivenAuthorIdIsNull_WhenCreatingManga()
    {
        var manga = new
        {
            Name = "Title",
            AuthorId = string.Empty
        };
        manga = manga with { AuthorId = null };

        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", manga);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GivenMangaWasCreated_WhenRetrievingById()
    {
        //Given
        const string mangaName = "Some manga title";
        const string authorId = "any-nonempty-string";
        var manga = new
        {
            Name = mangaName,
            AuthorId = authorId
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
        mangaRetrieved.AuthorId.Should().BeEquivalentTo(authorId);
        mangaRetrieved.Description.Should().BeEmpty();
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

    [Fact]
    public async Task GivenDescription_WhenChangingDescription()
    {
        var manga = new
        {
            Name = "Some manga title",
            AuthorId = "any-nonempty-string"
        };
        const string description = "A description of a manga.";

        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("create-manga", manga);
        var magnaId = await response.Content.ReadFromJsonAsync<Guid>();

        using HttpResponseMessage response2 = await _httpClient.PutAsJsonAsync($"manga/{magnaId}/change-description", description);
        response2.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var magna = await _httpClient.GetFromJsonAsync<MangaDto>($"manga/{magnaId}");

        magna.Should().NotBeNull();
        magna.Description.Should().BeEquivalentTo(description);
    }

    private class MangaDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string AuthorId { get; set; }

        public string Description { get; set; }
    }
}