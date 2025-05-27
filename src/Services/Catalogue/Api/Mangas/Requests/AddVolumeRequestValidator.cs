using FluentValidation;

namespace MangaShelf.Catalogue.Api.Mangas.Requests;

public class AddVolumeRequestValidator : AbstractValidator<AddVolumeRequest>
{
    public AddVolumeRequestValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty().WithMessage($"{nameof(AddVolumeRequest.Title)} is required.");

    }
}