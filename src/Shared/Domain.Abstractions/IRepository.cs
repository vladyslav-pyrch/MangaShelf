namespace MangaShelf.Domain.Abstractions;

public interface IRepository<TAggregate, TId> where TId : Identity where TAggregate : IAggregateRoot<TId>
{
    public Task<TId> NewId();
}