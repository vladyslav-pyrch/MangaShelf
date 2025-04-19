namespace MangaShelf.Domain.Abstractions;

/// <summary>
/// Abstract record for domain events that insures <see cref="OccuredOn"/> assignment.
/// </summary>
/// <param name="OccuredOn">Time when domain event was published</param>
/// <inheritdoc cref="IDomainEvent"/>
/// <seealso cref="IDomainEvent"/>
public abstract record DomainEvent(DateTime OccuredOn) : IDomainEvent
{
    public DateTime OccuredOn { get; } = OccuredOn;
}