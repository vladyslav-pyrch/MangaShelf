using FluentValidation.Results;
using MangaShelf.Application.Abstractions;

namespace MangaShelf.Catalogue.Application.Extensions;

public static class ValidationExtensions
{
    public static Result<T> ToFailure<T>(this ValidationResult result)
    {
        if (result.IsValid)
            throw new ArgumentException("Validation result should not be valid to be converted to Result<T>.Failure.");

        Error[] errors = result.Errors.Select(error => new Error(error.ErrorCode, error.ErrorMessage)).ToArray();
        return Result<T>.Failure(errors);
    }
}