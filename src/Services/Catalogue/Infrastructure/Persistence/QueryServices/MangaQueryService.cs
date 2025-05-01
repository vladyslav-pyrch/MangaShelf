using MangaShelf.Catalogue.Application.Features.Mangas.Dtos;
using MangaShelf.Catalogue.Application.Features.Mangas.Queries;
using MangaShelf.Catalogue.Domain.Mangas;
using MangaShelf.Catalogue.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace MangaShelf.Catalogue.Infrastructure.Persistence.QueryServices;

public class MangaQueryService(CatalogueDbContext dbContext) : IMangaQueryService
{
    public async Task<MangaDto> GetMangaById(MangaId mangaId, CancellationToken cancellationToken = default)
    {
        MangaEntity mangaEntity = await dbContext.Mangas.FirstAsync(
            entity => entity.Id == mangaId.Value, cancellationToken
        );

        return new MangaDto
        {
            Id = mangaEntity.Id,
            Name = mangaEntity.Name
        };
    }
}