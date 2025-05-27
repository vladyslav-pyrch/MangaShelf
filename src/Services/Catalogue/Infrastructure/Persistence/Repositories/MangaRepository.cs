using MangaShelf.Catalogue.Application.Exceptions;
using MangaShelf.Catalogue.Domain.Mangas;
using MangaShelf.Catalogue.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace MangaShelf.Catalogue.Infrastructure.Persistence.Repositories;

public class MangaRepository(CatalogueDbContext dbContext) : IMangaRepository
{
    public MangaId GenerateId() => new(Guid.CreateVersion7());

    public async Task<Manga> Read(MangaId id, CancellationToken cancellationToken = default)
    {
        MangaEntity? mangaEntity = await dbContext.Mangas.AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Id == id.Value, cancellationToken);

        NotFoundException.ThrowIfNull(mangaEntity);

        var manga = new Manga(
            new MangaId(mangaEntity.Id),
            mangaEntity.Name,
            new Author(mangaEntity.AuthorId),
            mangaEntity.Description
        );

        return manga;
    }

    public async Task Write(Manga manga, CancellationToken cancellationToken = default)
    {
        var mangaEntity = new MangaEntity
        {
            Id = manga.Id.Value,
            Name = manga.Title,
            AuthorId = manga.Author.Id,
            Description = manga.Description
        };

        if (dbContext.Mangas.AsNoTracking().Any(entity => entity.Id == mangaEntity.Id))
            dbContext.Mangas.Update(mangaEntity);
        else
            await dbContext.Mangas.AddAsync(mangaEntity, cancellationToken);
    }

    public Task Save(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}