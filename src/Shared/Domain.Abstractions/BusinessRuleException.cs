namespace MangaShelf.Domain.Abstractions;

/// <summary>
/// Base class for a business rule exceptions.
/// </summary>
/// <param name="message">Message that explains what business rule is broken.</param>
public class BusinessRuleException(string? message = null) : Exception(message)
{
    public static void ThrowIfNull(object obj, string? message = null)
    {
        if (obj == null) throw new BusinessRuleException(message);
    }

    public static void ThrowIfNullOrEmpty(string str, string? message = null)
    {
        if (string.IsNullOrEmpty(str)) throw new BusinessRuleException(message);
    }

    public static void ThrowIfNullOrWhiteSpace(string str, string? message = null)
    {
        if (string.IsNullOrWhiteSpace(str)) throw new BusinessRuleException(message);
    }
}