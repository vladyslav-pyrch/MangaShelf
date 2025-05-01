using MangaShelf.Catalogue.Application.Features.Mangas.Dtos;
using MangaShelf.Catalogue.Domain.Mangas;

namespace MangaShelf.Catalogue.Application.Features.Mangas.Queries;

public interface IMangaQueryService
{
    public Task<MangaDto?> GetMangaById(MangaId mangaId, CancellationToken cancellationToken = default);
}