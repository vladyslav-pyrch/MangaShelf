using MangaShelf.Domain.Abstractions;

namespace MangaShelf.Catalogue.Domain.Mangas;

public interface IMangaRepository : IRepository<Manga, MangaId>
{
    /// <summary>
    /// Generates a new unique identifier for a Manga entity.
    /// </summary>
    /// <returns><see cref="MangaId"/>A new unique identifier for a Manga entity.</returns>
    public MangaId GenerateId();

    /// <summary>
    /// Retrieves a Manga entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Manga entity to be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns><see cref="Manga"/>An entity associated with the specified identifier.</returns>
    public Task<Manga> Read(MangaId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Writes the given Manga entity to the data store.
    /// </summary>
    /// <param name="manga">The <see cref="Manga"/>An entity to be written to the data store.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    public Task Write(Manga manga, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves all pending changes to the data store.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the save operation.</param>
    public Task Save(CancellationToken cancellationToken = default);
}