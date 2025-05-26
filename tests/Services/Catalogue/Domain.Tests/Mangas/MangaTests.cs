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
    public void GivenNameIsNull_WhenCreatingManga_ThenThrowBusinessRuleException()
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
    public void GivenNameIsEmpty_WhenCreatingManga_ThenThrowBusinessRuleException()
    {
        _name = string.Empty;

        Func<Manga> when = () => Manga.Create(_id, _name, _author);

        when.Should().Throw<BusinessRuleException>()
            .Which.Message.Should().Be($"{nameof(Manga.Name)} cannot be null or whitespace.");
    }

    [Fact]
    public void GivenNameIsWhiteSpace_WhenCreatingManga_ThenThrowBusinessRuleException()
    {
        _name = "   ";

        Func<Manga> when = () => Manga.Create(_id, _name, _author);

        when.Should().Throw<BusinessRuleException>()
            .Which.Message.Should().Be($"{nameof(Manga.Name)} cannot be null or whitespace.");
    }

    [Fact]
    public void GivenNameIsLongerThan50Characters_WhenCreatingManga_ThenThrowBusinessRuleException()
    {
        _name = new string('a', 51);

        Func<Manga> when = () => Manga.Create(_id, _name, _author);

        BusinessRuleException? which = when.Should().Throw<BusinessRuleException>().Which;
        which.Message.Should().Be($"{nameof(Manga.Name)} should not be longer than 50 characters.");
        testOutputHelper.WriteLine(which.Message);
    }

    [Fact]
    public void GivenDescription_WhenChangingDescription_ThenDescriptionChanged()
    {
        var manga = Manga.Create(_id, _name, _author);

        manga.ChangeDescription("Some description");

        manga.Description.Should().BeEquivalentTo("Some description");
    }

    [Fact]
    public void GivenDescriptionIsNull_WhenChangingDescription_ThenThrowsBusinessRuleException()
    {
        var manga = Manga.Create(_id, _name, _author);

        Action when = () => manga.ChangeDescription(null!);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenDescriptionIsLongerThan500Characters_WhenChangingDescription_ThenThrowBusinessRuleException()
    {
        var manga = Manga.Create(_id, _name, _author);
        string description = new('a', 5001);

        Action when = () => manga.ChangeDescription(description);

        when.Should().Throw<BusinessRuleException>();
    }
}