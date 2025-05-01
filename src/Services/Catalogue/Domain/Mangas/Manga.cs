using MangaShelf.Domain.Abstractions;

namespace MangaShelf.Catalogue.Domain.Mangas;

public class Manga(MangaId id, string name) : AggregateRoot<MangaId>(id)
{
    public string Name { get; } = name;

    public static Manga Create(MangaId id, string name) => new(id, name);
}