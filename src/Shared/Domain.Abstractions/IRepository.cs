namespace MangaShelf.Domain.Abstractions;

/// <summary>
/// Interface for a repository of an <see cref="IAggregateRoot{TId}"/>.
/// </summary>
/// <typeparam name="TAggregate">Type of aggregate that a repository is for.</typeparam>
/// <typeparam name="TId">Type of ID of the aggregate.</typeparam>
/// <seealso cref="IAggregateRoot{TId}"/>
/// <seealso cref="Identity"/>
public interface IRepository<TAggregate, TId> where TId : Identity where TAggregate : IAggregateRoot<TId>;