using FluentValidation;

namespace MangaShelf.Catalogue.Api.Mangas.Requests;

public class CreateMangaRequestValidator : AbstractValidator<CreateMangaRequest>
{
    public CreateMangaRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage($"{nameof(CreateMangaRequest.Name)} is required.")
            .MaximumLength(50).WithMessage($"{nameof(CreateMangaRequest.Name)} must not exceed 50 characters.");

        RuleFor(request => request.AuthorId)
            .NotEmpty().WithMessage($"{nameof(CreateMangaRequest.AuthorId)} is required.");
    }
}