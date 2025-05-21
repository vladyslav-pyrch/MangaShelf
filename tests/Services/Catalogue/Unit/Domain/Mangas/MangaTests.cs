using System.Text;
using FluentAssertions;
using MangaShelf.Catalogue.Domain.Mangas;
using MangaShelf.Domain.Abstractions;
using Xunit.Abstractions;

namespace MangaShelf.Catalogue.Domain.Tests.Mangas;

public class MangaTests(ITestOutputHelper testOutputHelper)
{
    private MangaId _id = new(Guid.NewGuid());

    private string _name = "Some name";

    private Author _author = new("Author");

    [Fact]
    public void GivenNameIsNull_WhenCreatingManga_ThenThrowArgumentNullException()
    {
        // Given
        _name = null!;

        // When
        Func<Manga> when = () => Manga.Create(_id, _name, _author);

        // Then
        when.Should().Throw<BusinessRuleException>()
            .Which.Message.Should().Be($"{nameof(Manga.Name)} cannot be null or whitespace.");
    }

    [Fact]
    public void GivenNameIsEmpty_WhenCreatingManga_ThenThrowArgumentException()
    {
        _name = string.Empty;

        Func<Manga> when = () => Manga.Create(_id, _name, _author);

        when.Should().Throw<BusinessRuleException>()
            .Which.Message.Should().Be($"{nameof(Manga.Name)} cannot be null or whitespace.");
    }

    [Fact]
    public void GivenNameIsWhiteSpace_WhenCreatingManga_ThenThrowArgumentException()
    {
        _name = "   ";

        Func<Manga> when = () => Manga.Create(_id, _name, _author);

        when.Should().Throw<BusinessRuleException>()
            .Which.Message.Should().Be($"{nameof(Manga.Name)} cannot be null or whitespace.");
    }

    [Fact]
    public void GivenNameIsLongerThan50Characters_WhenCreatingManga_ThenThrowArgumentException()
    {
        _name = new string('a', 51);

        Func<Manga> when = () => Manga.Create(_id, _name, _author);

        BusinessRuleException? which = when.Should().Throw<BusinessRuleException>().Which;
        which.Message.Should().Be($"{nameof(Manga.Name)} should not be longer than 50 characters.");
        testOutputHelper.WriteLine(which.Message);
    }
}