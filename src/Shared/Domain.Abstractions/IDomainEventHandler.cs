namespace MangaShelf.Domain.Abstractions;

/// <summary>
/// Interface for domain event handlers.
/// </summary>
/// <typeparam name="TDomainEvent">Type of <see cref="IDomainEvent"/> to be handled.</typeparam>
/// <seealso cref="IDomainEvent"/>
public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
{
    /// <summary>
    /// Method that handles <see cref="IDomainEvent"/>
    /// </summary>
    /// <param name="domainEvent"><see cref="IDomainEvent"/> to be handled.</param>
    /// <returns></returns>
    public Task Handle(TDomainEvent domainEvent);
}