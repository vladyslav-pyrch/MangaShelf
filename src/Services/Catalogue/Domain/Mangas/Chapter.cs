using MangaShelf.Domain.Abstractions;

namespace MangaShelf.Catalogue.Domain.Mangas;

public class Chapter : Entity<ChapterId>
{
    private string _title = null!;

    private Chapter(ChapterId id, string title) : base(id)
    {
        Title = title;
    }

    internal static Chapter Create(ChapterId id, string title) => new(id, title);

    public string Title
    {
        get => _title;
        private set
        {
            BusinessRuleException.ThrowIfNullOrWhiteSpace(value, $"{nameof(Title)} may not be null or empty.");

            _title = value;
        }
    }

    public void ChangeTitle(string newTitle) => Title = newTitle;
}