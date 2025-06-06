﻿using FluentValidation;
using FluentValidation.Results;
using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Application.Extensions;
using MangaShelf.Catalogue.Domain.Mangas;

namespace MangaShelf.Catalogue.Application.Features.Mangas.Commands.CreateManga;

public class CreateMangaCommandHandler(IMangaRepository mangaRepository, IValidator<CreateMangaCommand> validator)
    : ICommandHandler<CreateMangaCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateMangaCommand command, CancellationToken cancellationToken)
    {
        ValidationResult result = await validator.ValidateAsync(command, cancellationToken);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        if (!result.IsValid)
            return result.ToFailure<Guid>();

        string name = command.Title;
        MangaId id = mangaRepository.GenerateId();
        var author = new Author(command.AuthorId);

        var manga = Manga.Create(id, name, author);

        await mangaRepository.Write(manga, cancellationToken);
        await mangaRepository.Save(cancellationToken);

        return manga.Id.Value;
    }
}