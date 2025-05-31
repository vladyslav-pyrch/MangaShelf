using FluentAssertions;
using MangaShelf.Catalogue.Domain.Mangas;

namespace MangaShelf.Catalogue.Domain.Tests.Mangas;

public class ChapterTests
{
    private readonly Manga _manga;

    public ChapterTests()
    {
        var id = new MangaId(Guid.NewGuid());
        var title = "Some title";
        var author = new Author("Author");

        _manga = Manga.Create(id, title, author);
    }

    [Fact]
    public void GivenChapterTitleAndId_WhenAddingChapter_ThenChapterIsAdded()
    {
        var chapterId = new ChapterId(Guid.CreateVersion7());
        var chapterTitle = "Chapter number 1";

        _manga.AddChapter(chapterId, chapterTitle);

        _manga.Chapters.Should().Contain(chapter => chapter.Id == chapterId && chapter.Title.Equals(chapterTitle));
    }
}