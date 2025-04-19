namespace MangaShelf.Domain.Abstractions;

/// <summary>
/// Base class for an Identity type that is required by <see cref="IEntity{TId}"/>
/// </summary>
public abstract record Identity : ValueObject;

/// <summary>
/// Extends <see cref="Identity"/> and adds generic functionality.
/// </summary>
/// <param name="Value">A value to be stored as an id.</param>
/// <typeparam name="T">type of id value.</typeparam>
public abstract record Identity<T>(T Value) : Identity where T : notnull
{
    public override string ToString() => Value.ToString() ?? string.Empty;
}