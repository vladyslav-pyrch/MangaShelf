using MangaShelf.Domain.Abstractions;

namespace MangaShelf.Catalogue.Domain.Mangas;

public class Manga : AggregateRoot<MangaId>
{
    private readonly string _name = null!;

    private readonly Author _author = null!;

    private string _description = null!;

    public Manga(MangaId id, string name, Author author, string description): base(id)
    {
        Name = name;
        Author = author;
        Description = description;
    }

    public string Name
    {
        get => _name;
        private init
        {
            BusinessRuleException.ThrowIfNullOrWhiteSpace(value, $"{nameof(Name)} cannot be null or whitespace.");

            if (value.Length > 50)
                throw new BusinessRuleException($"{nameof(Name)} should not be longer than 50 characters.");

            _name = value;
        }
    }

    public Author Author
    {
        get => _author;
        private init => _author = value;
    }

    public string Description
    {
        get => _description;
        private set => _description = value;
    }

    public static Manga Create(MangaId id, string name, Author author) => new(id, name, author, "");
}