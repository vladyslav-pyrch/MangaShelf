namespace MangaShelf.Domain.Abstractions;

/// <summary>
/// Base class for a business rule exceptions.
/// </summary>
/// <param name="message">Message that explains what business rule is broken.</param>
public class BusinessRuleException(string? message) : Exception(message);