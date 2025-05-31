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

    [Fact]
    public void GivenChapterWithTheSameIdExists_WhenAddingChapter_ThenThrowBusinessRuleException()
    {
        var guid = Guid.CreateVersion7();

        var chapterId1 = new ChapterId(guid);
        var chapterTitle1 = "Title1";

        _manga.AddChapter(chapterId1, chapterTitle1);

        var chapterId2 = new ChapterId(guid);
        var chapterTitle2 = "Title2";

        Action when = () => _manga.AddChapter(chapterId2, chapterTitle2);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenChapterWithTheSameTitleExists_WhenAddingChapter_ThenThrowBusinessRuleException()
    {
        var title = "Title";

        var chapterId1 = new ChapterId(Guid.CreateVersion7());
        var chapterTitle1 = new string(title);

        _manga.AddChapter(chapterId1, chapterTitle1);

        var chapterId2 = new ChapterId(Guid.CreateVersion7());
        var chapterTitle2 = new string(title);

        Action when = () => _manga.AddChapter(chapterId2, chapterTitle2);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenChapterIsAdded_WhenGettingChapterById_ThenReturnChapterWithTheId()
    {
        var chapterId = new ChapterId(Guid.CreateVersion7());
        var chapterTitle = "Title";

        _manga.AddChapter(chapterId, chapterTitle);

        Chapter chapter = _manga.GetChapter(chapterId);

        chapter.Id.Should().Be(chapterId);
    }

    [Fact]
    public void GivenChapterIsNotAdded_WhenGettingChapterById_ThenThrowBusinessRuleException()
    {
        var chapterId = new ChapterId(Guid.CreateVersion7());

        Action when = () => _manga.GetChapter(chapterId);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenChapterIsAdded_WhenRemovingChapterById_ThenChapterIsRemoved()
    {
        var chapterId = new ChapterId(Guid.CreateVersion7());
        var chapterTitle = "Title";

        _manga.AddChapter(chapterId, chapterTitle);

        _manga.Chapters.Should().Contain(chapter => chapter.Id == chapterId);

        _manga.RemoveChapter(chapterId);

        _manga.Chapters.Should().NotContain(chapter => chapter.Id == chapterId);
    }
}