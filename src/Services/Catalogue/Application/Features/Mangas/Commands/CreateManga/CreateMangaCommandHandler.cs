using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Domain.Mangas;

namespace MangaShelf.Catalogue.Application.Features.Mangas.Commands.CreateManga;

public class CreateMangaCommandHandler : ICommandHandler<CreateMangaCommand, Guid>
{
    public Task<Guid> Handle(CreateMangaCommand command, CancellationToken cancellationToken)
    {
        string name = command.Name;
        var id = new MangaId(Guid.NewGuid());

        var manga = Manga.Create(id, name);

        return Task.FromResult(manga.Id.Value);
    }
}