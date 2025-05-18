using MangaShelf.Domain.Abstractions;

namespace MangaShelf.Catalogue.Domain.Mangas;

public class Manga : AggregateRoot<MangaId>
{
    private readonly string _name = null!;

    public Manga(MangaId id, string name): base(id) => Name = name;

    public string Name
    {
        get => _name;
        private init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Name));

            if (value.Length > 50)
                throw new ArgumentException($"{nameof(Name)} should not be longer than 50 characters.", nameof(Name));

            _name = value;
        }
    }

    public static Manga Create(MangaId id, string name) => new(id, name);
}