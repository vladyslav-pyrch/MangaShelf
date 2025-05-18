using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MangaShelf.Catalogue.Api.Extensions;

public static class ValidatorExtensions
{
    public static void MoveToModelStateDictionary(this ValidationResult validationResult,
        ModelStateDictionary modelStateDictionary)
    {
        foreach ((string? key, string[]? errors) in validationResult.ToDictionary())
            foreach (string error in errors ?? [])
                modelStateDictionary.AddModelError(key ?? "", error);
    }

    public static async Task ValidateAsync<T>(this IValidator<T> validator, T instance, ModelStateDictionary modelState,
        CancellationToken cancellationToken = default) =>
        (await validator.ValidateAsync(instance, cancellationToken)).MoveToModelStateDictionary(modelState);

    public static void Validate<T>(this IValidator<T> validator, T instance, ModelStateDictionary modelState) =>
        validator.Validate(instance).MoveToModelStateDictionary(modelState);
}