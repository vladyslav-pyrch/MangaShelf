namespace MangaShelf.Domain.Abstractions;

public interface IDomainEvent
{
    public DateTime OccuredOn { get; }
}