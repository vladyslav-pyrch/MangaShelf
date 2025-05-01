using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Application.Features.Mangas.Commands.CreateManga;
using Microsoft.AspNetCore.Mvc;

namespace MangaShelf.Catalogue.Api.Mangas;

[ApiController]
public class MangaController(ICommandDispatcher commandDispatcher) : ControllerBase
{
    [HttpPost("create-manga")]
    public async Task<IActionResult> CreateManga([FromBody] CreateMangaDto createManga)
    {
        Guid id = await commandDispatcher.Dispatch<CreateMangaCommand, Guid>(
            new CreateMangaCommand(createManga.Name)
        );

        return Created($"/manga/{id}", id);
    }
}