using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Domain.Mangas;

namespace MangaShelf.Catalogue.Application.Features.Mangas.Commands.CreateManga;

public class CreateMangaCommandHandler(IMangaRepository mangaRepository) : ICommandHandler<CreateMangaCommand, Guid>
{
    public async Task<Guid> Handle(CreateMangaCommand command, CancellationToken cancellationToken)
    {
        string name = command.Name;
        MangaId id = mangaRepository.GenerateId();
        var manga = Manga.Create(id, name);

        await mangaRepository.Write(manga, cancellationToken);
        await mangaRepository.Save(cancellationToken);

        return manga.Id.Value;
    }
}