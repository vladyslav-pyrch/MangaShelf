using FluentValidation;
using FluentValidation.Results;
using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Application.Extensions;
using MangaShelf.Catalogue.Domain.Mangas;

namespace MangaShelf.Catalogue.Application.Features.Mangas.Commands.ChangeDescription;

public class ChangeDescriptionCommandHandler(IValidator<ChangeDescriptionCommand> validator,
    IMangaRepository repository)
    : ICommandHandler<ChangeDescriptionCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(ChangeDescriptionCommand command, CancellationToken cancellationToken = default)
    {
        ValidationResult result = await validator.ValidateAsync(command, cancellationToken);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        if (!result.IsValid)
            return result.ToFailure<Unit>();

        MangaId mangaId = new(command.MangaId);
        Manga manga = await repository.Read(mangaId, cancellationToken);

        manga.ChangeDescription(command.Description);

        await repository.Write(manga, cancellationToken);
        await repository.Save(cancellationToken);

        return Unit.Instance;
    }
}