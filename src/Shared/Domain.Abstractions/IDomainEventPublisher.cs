namespace MangaShelf.Domain.Abstractions;

public interface IDomainEventPublisher
{
    public Task Publish(IDomainEvent domainEvent);

    public Task Publish(IEnumerable<IDomainEvent> domainEvents);
}