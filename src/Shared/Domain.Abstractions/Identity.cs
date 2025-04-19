namespace MangaShelf.Domain.Abstractions;

public abstract record Identity : ValueObject;

public abstract record Identity<T>(T Value) : Identity where T : notnull
{
    public override string ToString() => Value.ToString() ?? string.Empty;
}