namespace MangaShelf.Domain.Abstractions;

public abstract class Entity<TId> : IEntity<TId> where TId : Identity
{
    private readonly TId _id = null!;

    protected Entity(TId id) => Id = id;

    public TId Id
    {
        get => _id;
        private init => _id = value ?? throw new BusinessRuleException("Id may not be null.");
    }
}