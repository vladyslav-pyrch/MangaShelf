using FluentAssertions;
using MangaShelf.Catalogue.Domain.Mangas;
using MangaShelf.Domain.Abstractions;
using Xunit.Abstractions;

namespace MangaShelf.Catalogue.Domain.Tests.Mangas;

public class MangaTests(ITestOutputHelper testOutputHelper)
{
    private MangaId _id = new(Guid.NewGuid());

    private string _title = "Some title";

    private Author _author = new("Author");

    [Fact]
    public void GivenTitleIsNull_WhenCreatingManga_ThenThrowBusinessRuleException()
    {
        // Given
        _title = null!;

        // When
        Func<Manga> when = () => Manga.Create(_id, _title, _author);

        // Then
        when.Should().Throw<BusinessRuleException>()
            .Which.Message.Should().Be($"{nameof(Manga.Title)} cannot be null or whitespace.");
    }

    [Fact]
    public void GivenTitleIsEmpty_WhenCreatingManga_ThenThrowBusinessRuleException()
    {
        _title = string.Empty;

        Func<Manga> when = () => Manga.Create(_id, _title, _author);

        when.Should().Throw<BusinessRuleException>()
            .Which.Message.Should().Be($"{nameof(Manga.Title)} cannot be null or whitespace.");
    }

    [Fact]
    public void GivenTitleIsWhiteSpace_WhenCreatingManga_ThenThrowBusinessRuleException()
    {
        _title = "   ";

        Func<Manga> when = () => Manga.Create(_id, _title, _author);

        when.Should().Throw<BusinessRuleException>()
            .Which.Message.Should().Be($"{nameof(Manga.Title)} cannot be null or whitespace.");
    }

    [Fact]
    public void GivenTitleIsLongerThan50Characters_WhenCreatingManga_ThenThrowBusinessRuleException()
    {
        _title = new string('a', 51);

        Func<Manga> when = () => Manga.Create(_id, _title, _author);

        BusinessRuleException? which = when.Should().Throw<BusinessRuleException>().Which;
        which.Message.Should().Be($"{nameof(Manga.Title)} should not be longer than 50 characters.");
        testOutputHelper.WriteLine(which.Message);
    }

    [Fact]
    public void GivenDescription_WhenChangingDescription_ThenDescriptionChanged()
    {
        var manga = Manga.Create(_id, _title, _author);

        manga.ChangeDescription("Some description");

        manga.Description.Should().BeEquivalentTo("Some description");
    }

    [Fact]
    public void GivenDescriptionIsNull_WhenChangingDescription_ThenThrowsBusinessRuleException()
    {
        var manga = Manga.Create(_id, _title, _author);

        Action when = () => manga.ChangeDescription(null!);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenDescriptionIsLongerThan500Characters_WhenChangingDescription_ThenThrowBusinessRuleException()
    {
        var manga = Manga.Create(_id, _title, _author);
        string description = new('a', 5001);

        Action when = () => manga.ChangeDescription(description);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenAuthorIsNull_WhenCreatingManga_ThenThrowBusinessRuleException()
    {
        _author = null!;

        Action when = () => Manga.Create(_id, _title, _author);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenNewTitle_WhenChangingTitle_ThenTitleIsChanged()
    {
        var manga = Manga.Create(_id, _title, _author);
        var newTitle = "New title";

        manga.ChangeTitle(newTitle);

        manga.Title.Should().BeEquivalentTo(newTitle);
    }

    [Fact]
    public void GivenChapterWithTheSameIdExists_WhenAddingChapter_ThenThrowBusinessRuleException()
    {
        var manga = Manga.Create(_id, _title, _author);
        var guid = Guid.CreateVersion7();

        var chapterId1 = new ChapterId(guid);
        var chapterTitle1 = "Title1";

        manga.AddChapter(chapterId1, chapterTitle1);

        var chapterId2 = new ChapterId(guid);
        var chapterTitle2 = "Title2";

        Action when = () => manga.AddChapter(chapterId2, chapterTitle2);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenChapterWithTheSameTitleExists_WhenAddingChapter_ThenThrowBusinessRuleException()
    {
        var manga = Manga.Create(_id, _title, _author);
        var title = "Title";

        var chapterId1 = new ChapterId(Guid.CreateVersion7());
        var chapterTitle1 = new string(title);

        manga.AddChapter(chapterId1, chapterTitle1);

        var chapterId2 = new ChapterId(Guid.CreateVersion7());
        var chapterTitle2 = new string(title);

        Action when = () => manga.AddChapter(chapterId2, chapterTitle2);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenChapterIsAdded_WhenGettingChapterById_ThenReturnChapterWithTheId()
    {
        var manga = Manga.Create(_id, _title, _author);
        var chapterId = new ChapterId(Guid.CreateVersion7());
        var chapterTitle = "Title";

        manga.AddChapter(chapterId, chapterTitle);

        Chapter chapter = manga.GetChapter(chapterId);

        chapter.Id.Should().Be(chapterId);
    }
}