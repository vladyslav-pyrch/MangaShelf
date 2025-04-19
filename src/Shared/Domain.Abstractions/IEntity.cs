namespace MangaShelf.Domain.Abstractions;

public interface IEntity<out TId> where TId : Identity
{
    public TId Id { get; }
}