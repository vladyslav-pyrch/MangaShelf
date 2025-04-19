namespace MangaShelf.Domain.Abstractions;

public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
{
    public Task Handle(TDomainEvent domainEvent);
}