using FluentValidation;

namespace MangaShelf.Catalogue.Api.Mangas.Requests;

public class CreateMangaRequestValidator : AbstractValidator<CreateMangaRequest>
{
    public CreateMangaRequestValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty().WithMessage($"{nameof(CreateMangaRequest.Title)} is required.")
            .MaximumLength(50).WithMessage($"{nameof(CreateMangaRequest.Title)} must not exceed 50 characters.");

        RuleFor(request => request.AuthorId)
            .NotEmpty().WithMessage($"{nameof(CreateMangaRequest.AuthorId)} is required.");
    }
}