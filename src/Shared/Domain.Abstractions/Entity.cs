namespace MangaShelf.Domain.Abstractions;

/// <summary>
/// Abstract class for an entity that insures <see cref="Id"/> asignment.
/// </summary>
/// <inheritdoc cref="IEntity{TId}"/>
public abstract class Entity<TId> : IEntity<TId> where TId : Identity
{
    private readonly TId _id = null!;

    /// <summary>
    /// Base constructor.
    /// </summary>
    /// <param name="id">ID of the entity.</param>
    /// <seealso cref="Identity"/>
    protected Entity(TId id) => Id = id;

    public TId Id
    {
        get => _id;
        private init => _id = value ?? throw new BusinessRuleException("Id may not be null.");
    }
}