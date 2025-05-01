using MangaShelf.Catalogue.Application.Exceptions;
using MangaShelf.Catalogue.Domain.Mangas;
using MangaShelf.Catalogue.Infrastructure.Persistence.Entities;

namespace MangaShelf.Catalogue.Infrastructure.Persistence.Repositories;

public class MangaRepository(CatalogueDbContext dbContext) : IMangaRepository
{
    public MangaId GenerateId() => new(Guid.CreateVersion7());

    public async Task<Manga> Read(MangaId id, CancellationToken cancellationToken = default)
    {
        MangaEntity? mangaEntity = await dbContext.Mangas.FindAsync([id.Value], cancellationToken: cancellationToken);

        NotFoundException.ThrowIfNull(mangaEntity);

        var mangaId = new MangaId(mangaEntity.Id);
        string name = mangaEntity.Name;
        var manga = new Manga(mangaId, name);

        return manga;
    }

    public async Task Write(Manga manga, CancellationToken cancellationToken = default)
    {
        var mangaEntity = new MangaEntity()
        {
            Id = manga.Id.Value,
            Name = manga.Name
        };

        await dbContext.Mangas.AddAsync(mangaEntity, cancellationToken);
    }

    public Task Save(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}