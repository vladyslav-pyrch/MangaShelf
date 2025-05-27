using MangaShelf.Domain.Abstractions;

namespace MangaShelf.Catalogue.Domain.Mangas;

public record VolumeId(Guid Value) : Identity<Guid>(Value);