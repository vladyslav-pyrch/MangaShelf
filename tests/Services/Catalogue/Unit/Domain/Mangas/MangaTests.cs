using FluentAssertions;
using MangaShelf.Catalogue.Domain.Mangas;
using Xunit.Abstractions;

namespace MangaShelf.Catalogue.Domain.Tests.Mangas;

public class MangaTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void GivenNameIsNull_WhenCreatingManga_ThenThrowArgumentNullException()
    {
        // Given
        var id = new MangaId(Guid.NewGuid());
        string name = null!;

        // When
        Func<Manga> when = () => Manga.Create(id, name);

        // Then
        ArgumentNullException? which = when.Should().Throw<ArgumentNullException>().Which;
        which.ParamName.Should().Be(nameof(Manga.Name));
        testOutputHelper.WriteLine(which.Message);
    }

    [Fact]
    public void GivenNameIsEmpty_WhenCreatingManga_ThenThrowArgumentException()
    {
        var id = new MangaId(Guid.NewGuid());
        var name = string.Empty;

        Func<Manga> when = () => Manga.Create(id, name);

        ArgumentException? which = when.Should().Throw<ArgumentException>().Which;
        which.ParamName.Should().Be(nameof(Manga.Name));
        testOutputHelper.WriteLine(which.Message);
    }

    [Fact]
    public void GivenNameIsWhiteSpace_WhenCreatingManga_ThenThrowArgumentException()
    {
        var id = new MangaId(Guid.NewGuid());
        var name = "   ";

        Func<Manga> when = () => Manga.Create(id, name);

        ArgumentException? which = when.Should().Throw<ArgumentException>().Which;
        which.ParamName.Should().Be(nameof(Manga.Name));
        testOutputHelper.WriteLine(which.Message);
    }
}