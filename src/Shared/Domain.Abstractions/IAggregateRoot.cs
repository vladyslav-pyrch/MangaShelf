namespace MangaShelf.Domain.Abstractions;

/// <summary>
/// Interface for an aggregate root that extends <see cref="IEntity{TId}"/> and adds access to domain events.
/// </summary>
/// <inheritdoc cref="IEntity{TId}"/>
/// <seealso cref="IEntity{TId}"/>
/// <seealso cref="Identity"/>
public interface IAggregateRoot<out TId> : IEntity<TId> where TId : Identity
{
    /// <summary>
    /// Read only collection of domain events.
    /// </summary>
    /// <seealso cref="IDomainEvent"/>
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Clears domain events.
    /// </summary>
    public void ClearDomainEvents();
}