using System.Text;
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

    [Fact]
    public void GivenNameIsLongerThan50Characters_WhenCreatingManga_ThenThrowArgumentException()
    {
        var id = new MangaId(Guid.NewGuid());
        var nameBuilder = new StringBuilder(51, 51);
        for (var i = 0; i < 5; i++)
            nameBuilder.Append("1234567890");
        nameBuilder.Append('1');
        var name = nameBuilder.ToString();

        Func<Manga> when = () => Manga.Create(id, name);

        ArgumentException? which = when.Should().Throw<ArgumentException>().Which;
        which.ParamName.Should().Be(nameof(Manga.Name));
        which.Message.Should().Contain($"{nameof(Manga.Name)} should not be longer than 50 characters.");
        testOutputHelper.WriteLine(which.Message);
    }
}