using FluentValidation;

namespace MangaShelf.Catalogue.Application.Features.Mangas.Commands.CreateManga;

public class CreateMangaCommandValidator : AbstractValidator<CreateMangaCommand>
{
    public CreateMangaCommandValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Name is required").WithErrorCode(ErrorCodes.InvalidMangaName)
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters").WithErrorCode(ErrorCodes.InvalidMangaName);
    }
}