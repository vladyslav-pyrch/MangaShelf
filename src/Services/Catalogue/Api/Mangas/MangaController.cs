using FluentValidation;
using FluentValidation.TestHelper;
using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Api.Extensions;
using MangaShelf.Catalogue.Api.Mangas.Requests;
using MangaShelf.Catalogue.Application.Features.Mangas.Commands.CreateManga;
using MangaShelf.Catalogue.Application.Features.Mangas.Dtos;
using MangaShelf.Catalogue.Application.Features.Mangas.Queries.GetMangaById;
using Microsoft.AspNetCore.Mvc;

namespace MangaShelf.Catalogue.Api.Mangas;

[ApiController]
public class MangaController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpPost("create-manga")]
    public async Task<IActionResult> CreateManga([FromBody] CreateMangaRequest createManga,
        [FromServices] IValidator<CreateMangaRequest> validator,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAsync(createManga, ModelState, cancellationToken);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Guid id = await commandDispatcher.Dispatch<CreateMangaCommand, Guid>(
            new CreateMangaCommand(createManga.Name), cancellationToken
        );

        return Created($"/manga/{id}", id);
    }

    [HttpGet("manga/{id:guid}")]
    public async Task<IActionResult> GetMangaById(Guid id, CancellationToken cancellationToken)
    {
        MangaDto? mangaDto = await queryDispatcher.Dispatch<GetMangaByIdQuery, MangaDto?>(
            new GetMangaByIdQuery(id), cancellationToken
        );

        if (mangaDto is null)
            return NotFound();

        return Ok(new { mangaDto.Id, mangaDto.Name });
    }
}