namespace MangaShelf.Domain.Abstractions;

/// <summary>
/// Base interface for an entity.
/// </summary>
/// <typeparam name="TId">Type of strongly typed id that inherits from <see cref="Identity"/>.</typeparam>
/// <see cref="Identity"/>
public interface IEntity<out TId> where TId : Identity
{
    /// <summary>
    /// ID of an entity
    /// </summary>
    /// <see cref="Identity"/>
    public TId Id { get; }
}