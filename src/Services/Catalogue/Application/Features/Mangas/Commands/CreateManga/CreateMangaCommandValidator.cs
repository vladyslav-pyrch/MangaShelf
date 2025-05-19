using FluentValidation;

namespace MangaShelf.Catalogue.Application.Features.Mangas.Commands.CreateManga;

public class CreateMangaCommandValidator : AbstractValidator<CreateMangaCommand>
{
    public CreateMangaCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty().MaximumLength(50);
    }
}