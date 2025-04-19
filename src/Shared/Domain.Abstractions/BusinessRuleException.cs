namespace MangaShelf.Domain.Abstractions;

public class BusinessRuleException(string? message) : Exception(message);