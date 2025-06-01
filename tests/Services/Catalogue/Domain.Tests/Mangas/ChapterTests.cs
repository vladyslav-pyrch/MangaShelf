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
        var number = 1;

        _manga.AddChapter(id, title, number);

        Chapter which = _manga.Chapters.Should().Contain(chapter => chapter.Id == id).Which;
        which.Title.Should().BeEquivalentTo(title);
        which.Number.Should().Be(number);
    }

    [Fact]
    public void GivenTitleIsNull_WhenAddingChapter_ThenThrowBusinessRuleException()
    {
        var id = new ChapterId(Guid.CreateVersion7());
        string title = null!;
        var number = 1;

        Action when = () => _manga.AddChapter(id, title, number);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenTitleIsEmpty_WhenAddingChapter_ThenThrowBusinessRuleException()
    {
        var id = new ChapterId(Guid.CreateVersion7());
        var title = string.Empty;
        var number = 1;

        Action when = () => _manga.AddChapter(id, title, number);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenNumberIsNegative_WhenAddingChapter_ThenThrowBusinessRuleException()
    {
        var id = new ChapterId(Guid.CreateVersion7());
        var title = "Title";
        var number = -1;

        Action when = () => _manga.AddChapter(id, title, number);

        when.Should().Throw<BusinessRuleException>();

    }

    [Fact]
    public void GivenNumberIsZero_WhenAddingChapter_ThenThrowBusinessRuleException()
    {
        var id = new ChapterId(Guid.CreateVersion7());
        var title = "Title";
        var number = 0;

        Action when = () => _manga.AddChapter(id, title, number);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenNewTitle_WhenChangingChapterTitle_ThenChapterTitleIsChanged()
    {
        var id = new ChapterId(Guid.CreateVersion7());
        var title = "Chapter number 1";
        var number = 1;

        _manga.AddChapter(id, title, number);

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
        var number1 = 1;

        _manga.AddChapter(chapterId1, chapterTitle1, number1);

        var chapterId2 = new ChapterId(guid);
        var chapterTitle2 = "Title2";
        var number2 = 2;

        Action when = () => _manga.AddChapter(chapterId2, chapterTitle2, number2);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenChapterWithTheSameTitleExists_WhenAddingChapter_ThenThrowBusinessRuleException()
    {
        var title = "Title";

        var chapterId1 = new ChapterId(Guid.CreateVersion7());
        var chapterTitle1 = new string(title);
        var number1 = 1;

        _manga.AddChapter(chapterId1, chapterTitle1, number1);

        var chapterId2 = new ChapterId(Guid.CreateVersion7());
        var chapterTitle2 = new string(title);
        var number2 = 2;

        Action when = () => _manga.AddChapter(chapterId2, chapterTitle2, number2);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenChapterWithTheSameNumberExists_WhenAddingChapter_ThenThrowBusinessRuleException()
    {
        var number = 1;

        var chapterId1 = new ChapterId(Guid.CreateVersion7());
        var chapterTitle1 = "Title 1";

        _manga.AddChapter(chapterId1, chapterTitle1, number);

        var chapterId2 = new ChapterId(Guid.CreateVersion7());
        var chapterTitle2 = "Title 2";

        Action when = () => _manga.AddChapter(chapterId2, chapterTitle2, number);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenChapterIsAdded_WhenGettingChapterById_ThenReturnChapterWithTheId()
    {
        var chapterId = new ChapterId(Guid.CreateVersion7());
        var chapterTitle = "Title";
        var number = 1;

        _manga.AddChapter(chapterId, chapterTitle, number);

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
        var number = 1;

        _manga.AddChapter(chapterId, chapterTitle, number);

        _manga.Chapters.Should().Contain(chapter => chapter.Id == chapterId);

        _manga.RemoveChapter(chapterId);

        _manga.Chapters.Should().NotContain(chapter => chapter.Id == chapterId);
    }

    [Fact]
    public void GivenChapterIsNotAdded_WhenRemovingChapterById_ThenThrowBusinessRuleException()
    {
        var chapterId = new ChapterId(Guid.CreateVersion7());

        Action when = () => _manga.RemoveChapter(chapterId);

        when.Should().Throw<BusinessRuleException>();
    }
}