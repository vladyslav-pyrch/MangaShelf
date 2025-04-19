namespace MangaShelf.Domain.Abstractions;

/// <summary>
/// Abstract class for an aggregate root that handles raising, clearing and giving access to <see cref="DomainEvents"/>
/// </summary>
/// <param name="id">ID of the aggregate.</param>
/// <inheritdoc cref="IAggregateRoot{TId}"/>
/// <seealso cref="Entity{TId}"/>
/// <seealso cref="IAggregateRoot{TId}"/>
public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id), IAggregateRoot<TId>
    where TId : Identity
{
    private readonly Queue<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    public void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>
    /// Adds a <see cref="IDomainEvent">domain event</see> to a queue of domain events.
    /// </summary>
    /// <param name="domainEvent">A domain event to be risen.</param>
    /// <seealso cref="IDomainEvent"/>
    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Enqueue(domainEvent);
}