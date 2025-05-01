using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Application.Features.Mangas.Dtos;
using MangaShelf.Catalogue.Domain.Mangas;

namespace MangaShelf.Catalogue.Application.Features.Mangas.Queries.GetMangaById;

public class GetMangaByIdQueryHandler(IMangaQueryService mangaQueryService) : IQueryHandler<GetMangaByIdQuery, MangaDto>
{
    public Task<MangaDto> Handle(GetMangaByIdQuery query, CancellationToken cancellationToken = default)
    {
        var mangaId = new MangaId(query.Id);

        return mangaQueryService.GetMangaById(mangaId, cancellationToken);
    }
}