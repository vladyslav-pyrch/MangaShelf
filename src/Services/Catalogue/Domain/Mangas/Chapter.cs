using MangaShelf.Domain.Abstractions;

namespace MangaShelf.Catalogue.Domain.Mangas;

public class Chapter : Entity<ChapterId>
{
    private string _title = null!;
    private int _number;

    private Chapter(ChapterId id, string title, int number) : base(id)
    {
        Title = title;
        Number = number;
    }

    internal static Chapter Create(ChapterId id, string title, int number) => new(id, title, number);

    public string Title
    {
        get => _title;
        private set
        {
            BusinessRuleException.ThrowIfNullOrWhiteSpace(value, $"{nameof(Title)} may not be null or empty.");

            _title = value;
        }
    }

    public int Number
    {
        get => _number;
        private set
        {
            if (value <= 0)
                throw new BusinessRuleException($"{nameof(value)} may not be zero or negative.");

            _number = value;
        }
    }

    internal void ChangeTitle(string newTitle) => Title = newTitle;
}