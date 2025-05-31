using MangaShelf.Domain.Abstractions;

namespace MangaShelf.Catalogue.Domain.Mangas;

public class Chapter(ChapterId id, string title) : Entity<ChapterId>(id)
{
    internal static Chapter Create(ChapterId id, string title) => new(id, title);

    public string Title { get; private set; } = title;
}