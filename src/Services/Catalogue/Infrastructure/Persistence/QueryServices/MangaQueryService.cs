using MangaShelf.Catalogue.Application.Features.Mangas.Dtos;
using MangaShelf.Catalogue.Application.Features.Mangas.Queries;
using MangaShelf.Catalogue.Domain.Mangas;
using MangaShelf.Catalogue.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace MangaShelf.Catalogue.Infrastructure.Persistence.QueryServices;

public class MangaQueryService(CatalogueDbContext dbContext) : IMangaQueryService
{
    public async Task<MangaDto?> GetMangaById(MangaId mangaId, CancellationToken cancellationToken = default)
    {
        MangaEntity? mangaEntity = await dbContext.Mangas.FirstOrDefaultAsync(
            entity => entity.Id == mangaId.Value, cancellationToken
        );

        return mangaEntity is null ? null : new MangaDto
        {
            Id = mangaEntity.Id,
            Name = mangaEntity.Name,
            Description = mangaEntity.Description,
            AuthorId = mangaEntity.AuthorId
        };
    }
}