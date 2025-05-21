namespace MangaShelf.Domain.Abstractions.Tests.TestImplementations;

public record TestDomainEvent() : DomainEvent(DateTime.Today);