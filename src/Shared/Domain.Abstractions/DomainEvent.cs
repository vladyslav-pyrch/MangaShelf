namespace MangaShelf.Domain.Abstractions;

public abstract record DomainEvent(DateTime OccuredOn) : IDomainEvent
{
    public DateTime OccuredOn { get; } = OccuredOn;
}