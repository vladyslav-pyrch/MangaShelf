using FluentValidation;

namespace MangaShelf.Catalogue.Api.Mangas.Requests;

public class AddVolumeRequestValidator : AbstractValidator<AddVolumeRequest>
{
    public AddVolumeRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage($"{nameof(AddVolumeRequest.Name)} is required.");

    }
}