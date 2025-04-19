namespace MangaShelf.Domain.Abstractions.Tests.TestImplementations;

public sealed record TestEntityId(Guid Value) : Identity<Guid>(Value);