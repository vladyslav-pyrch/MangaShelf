namespace MangaShelf.Application.Abstractions;

public readonly record struct Unit
{
    public static Unit Instance { get; } = new();
}