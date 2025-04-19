namespace MangaShelf.Domain.Abstractions;

/// <summary>
/// Interface for a domain event publisher
/// </summary>
/// <seealso cref="IDomainEvent"/>
public interface IDomainEventPublisher
{
    /// <summary>
    /// Published one <see cref="IDomainEvent">Domain event</see>.
    /// </summary>
    /// <param name="domainEvent"><see cref="IDomainEvent">Domain event</see> to be published</param>
    /// <returns></returns>
    /// <seealso cref="IDomainEvent"/>
    public Task Publish(IDomainEvent domainEvent);

    /// <summary>
    /// Published a list of domain events
    /// </summary>
    /// <param name="domainEvents"><see cref="IDomainEvent">Domain events</see> to be published</param>
    /// <returns></returns>
    /// <seealso cref="IDomainEvent"/>
    public Task Publish(IEnumerable<IDomainEvent> domainEvents);
}