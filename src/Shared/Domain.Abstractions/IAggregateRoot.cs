namespace MangaShelf.Domain.Abstractions;

public interface IAggregateRoot<TId> : IEntity<TId> where TId : Identity
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    public void ClearDomainEvents();
}