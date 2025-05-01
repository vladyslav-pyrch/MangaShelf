using System.Diagnostics.CodeAnalysis;

namespace MangaShelf.Catalogue.Application.Exceptions;

public class NotFoundException(string message = "") : Exception(message)
{
    public static void ThrowIfNull([NotNull] object? o, string message = "")
    {
        if (o is null)
            throw new NotFoundException(message);
    }
}