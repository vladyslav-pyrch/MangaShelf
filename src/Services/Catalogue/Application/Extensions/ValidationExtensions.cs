using FluentValidation.Results;
using MangaShelf.Application.Abstractions;

namespace MangaShelf.Catalogue.Application.Extensions;

public static class ValidationExtensions
{
    public static Result<T> ToFailure<T>(this ValidationResult result)
    {
        string[] errors = result.Errors.Select(error => error.ErrorMessage).ToArray();
        return Result<T>.Failure(string.Join(Environment.NewLine, errors));
    }
}