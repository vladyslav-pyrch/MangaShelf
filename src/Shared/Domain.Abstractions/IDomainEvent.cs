namespace MangaShelf.Domain.Abstractions;

/// <summary>
/// Base interface for domain events.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Time when the domain event occured.
    /// </summary>
    public DateTime OccuredOn { get; }
}