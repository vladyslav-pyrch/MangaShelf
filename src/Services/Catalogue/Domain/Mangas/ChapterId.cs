using MangaShelf.Domain.Abstractions;

namespace MangaShelf.Catalogue.Domain.Mangas;

public record ChapterId(Guid Value) : Identity<Guid>(Value);