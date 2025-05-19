using System.Diagnostics.CodeAnalysis;

namespace MangaShelf.Application.Abstractions;

public record Result<T>(
    [property: MemberNotNullWhen(true, "Value")]
    bool IsSuccess, T? Value, string? Error)
{
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailure => !IsSuccess;

    public static Result<T> Success(T value) => new(true, value, null);

    public static Result<T> Failure(string error) => new(false, default, error);

    public static implicit operator Result<T>(T value) => Success(value);
}