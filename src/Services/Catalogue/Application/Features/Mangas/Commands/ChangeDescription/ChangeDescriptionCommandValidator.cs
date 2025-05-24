using FluentValidation;

namespace MangaShelf.Catalogue.Application.Features.Mangas.Commands.ChangeDescription;

public class ChangeDescriptionCommandValidator : AbstractValidator<ChangeDescriptionCommand>
{
    public ChangeDescriptionCommandValidator()
    {
        RuleFor(command => command.Description)
            .NotNull().WithMessage("Description is required").WithErrorCode(ErrorCodes.InvalidMangaDescription);
    }
}