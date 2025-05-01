using MangaShelf.Domain.Abstractions;

namespace MangaShelf.Catalogue.Domain.Mangas;

public record MangaId(Guid Value) : Identity<Guid>(Value);