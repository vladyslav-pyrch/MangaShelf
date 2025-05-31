using MangaShelf.Domain.Abstractions;

namespace MangaShelf.Catalogue.Domain.Mangas;

public class Manga : AggregateRoot<MangaId>
{
    private string _title = null!;

    private readonly Author _author = null!;

    private string _description = null!;

    private readonly IDictionary<ChapterId, Chapter> _chapters = new Dictionary<ChapterId, Chapter>();

    public Manga(MangaId id, string title, Author author, string description): base(id)
    {
        Title = title;
        Author = author;
        Description = description;
    }

    public string Title
    {
        get => _title;
        private set
        {
            BusinessRuleException.ThrowIfNullOrWhiteSpace(value, $"{nameof(Title)} cannot be null or whitespace.");

            if (value.Length > 50)
                throw new BusinessRuleException($"{nameof(Title)} should not be longer than 50 characters.");

            _title = value;
        }
    }

    public Author Author
    {
        get => _author;
        private init
        {
            BusinessRuleException.ThrowIfNull(value, $"{nameof(Author)} cannot be null.");

            _author = value;
        }
    }

    public string Description
    {
        get => _description;
        private set
        {
            BusinessRuleException.ThrowIfNull(value, $"{nameof(Description)} cannot be null.");

            if (value.Length > 5000)
                throw new BusinessRuleException($"{nameof(Description)} should not be longer than 5000 characters.");

            _description = value;
        }
    }

    public ICollection<Chapter> Chapters => _chapters.Values;

    public static Manga Create(MangaId id, string title, Author author) => new(id, title, author, "");

    public void ChangeDescription(string description) => Description = description;

    public void ChangeTitle(string title) => Title = title;

    public void AddChapter(ChapterId chapterId, string title)
    {
        if (_chapters.ContainsKey(chapterId))
            throw new BusinessRuleException($"Chapter with the same id has already been added. (id = {chapterId})");

        if (_chapters.Values.Any(chapter => chapter.Title.Equals(title)))
            throw new BusinessRuleException($"Chapter with the same title has already been added. (title = {title})");

        var chapter = Chapter.Create(chapterId, title);

        _chapters.Add(chapterId, chapter);
    }

    public Chapter GetChapter(ChapterId chapterId)
    {
        if (_chapters.TryGetValue(chapterId, out Chapter? chapter))
            return chapter;

        throw new BusinessRuleException($"Chapter with such id has not been added. (id = {chapterId})");
    }

    public void RemoveChapter(ChapterId chapterId)
    {
        _chapters.Remove(chapterId);
    }

    public void ChangeChapterTitle(ChapterId chapterId, string newTitle) =>
        _chapters[chapterId].ChangeTitle(newTitle);
}