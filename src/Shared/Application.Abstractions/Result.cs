using System.Diagnostics.CodeAnalysis;

namespace MangaShelf.Application.Abstractions;

public record Result<T>(
    [property: MemberNotNullWhen(true, "Value")]
    bool IsSuccess, T? Value, Error[]? Errors)
{
    [MemberNotNullWhen(true, nameof(Errors))]
    public bool IsFailure => !IsSuccess;

    public static Result<T> Success(T value) => new(true, value, null);

    public static Result<T> Failure(Error[] errors) => new(false, default, errors);

    public static implicit operator Result<T>(T value) => Success(value);
}