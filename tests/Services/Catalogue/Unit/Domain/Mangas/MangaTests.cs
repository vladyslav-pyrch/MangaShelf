using System.Text;
using FluentAssertions;
using MangaShelf.Catalogue.Domain.Mangas;
using MangaShelf.Domain.Abstractions;
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
        when.Should().Throw<BusinessRuleException>()
            .Which.Message.Should().Be($"{nameof(Manga.Name)} cannot be null or whitespace.");
    }

    [Fact]
    public void GivenNameIsEmpty_WhenCreatingManga_ThenThrowArgumentException()
    {
        var id = new MangaId(Guid.NewGuid());
        var name = string.Empty;

        Func<Manga> when = () => Manga.Create(id, name);

        when.Should().Throw<BusinessRuleException>()
            .Which.Message.Should().Be($"{nameof(Manga.Name)} cannot be null or whitespace.");
    }

    [Fact]
    public void GivenNameIsWhiteSpace_WhenCreatingManga_ThenThrowArgumentException()
    {
        var id = new MangaId(Guid.NewGuid());
        var name = "   ";

        Func<Manga> when = () => Manga.Create(id, name);

        when.Should().Throw<BusinessRuleException>()
            .Which.Message.Should().Be($"{nameof(Manga.Name)} cannot be null or whitespace.");
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

        BusinessRuleException? which = when.Should().Throw<BusinessRuleException>().Which;
        which.Message.Should().Be($"{nameof(Manga.Name)} should not be longer than 50 characters.");
        testOutputHelper.WriteLine(which.Message);

    }
}