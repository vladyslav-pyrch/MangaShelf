using FluentAssertions;
using MangaShelf.Catalogue.Domain.Mangas;
using MangaShelf.Domain.Abstractions;

namespace MangaShelf.Catalogue.Domain.Tests.Mangas;

public class ChapterTests
{
    private readonly Manga _manga;

    public ChapterTests()
    {
        var mangaId = new MangaId(Guid.NewGuid());
        var mangaTitle = "Some title";
        var magnaAuthor = new Author("Author");

        _manga = Manga.Create(mangaId, mangaTitle, magnaAuthor);
    }

    [Fact]
    public void GivenTitleAndId_WhenAddingChapter_ThenChapterIsAdded()
    {
        var id = new ChapterId(Guid.CreateVersion7());
        var title = "Chapter number 1";

        _manga.AddChapter(id, title);

        _manga.Chapters.Should().Contain(chapter => chapter.Id == id && chapter.Title.Equals(title));
    }

    [Fact]
    public void GivenTitleIsNull_WhenAddingChapter_ThenThrowBusinessRuleException()
    {
        var id = new ChapterId(Guid.CreateVersion7());
        string title = null!;

        Action when = () => _manga.AddChapter(id, title);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenTitleIsEmpty_WhenAddingChapter_ThenThrowBusinessRuleException()
    {
        var id = new ChapterId(Guid.CreateVersion7());
        var title = string.Empty;

        Action when = () => _manga.AddChapter(id, title);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenNewTitle_WhenChangingChapterTitle_ThenChapterTitleIsChanged()
    {
        var id = new ChapterId(Guid.CreateVersion7());
        var title = "Chapter number 1";

        _manga.AddChapter(id, title);

        var newTitle = "Chapter number1: rebellion";

        _manga.ChangeChapterTitle(id, newTitle);

        _manga.Chapters.Should().Contain(chapter => chapter.Id == id)
            .Which.Title.Should().BeEquivalentTo(newTitle);
    }
}