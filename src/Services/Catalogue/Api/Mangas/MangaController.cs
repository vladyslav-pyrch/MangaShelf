using FluentValidation;
using FluentValidation.TestHelper;
using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Api.Extensions;
using MangaShelf.Catalogue.Api.Mangas.Requests;
using MangaShelf.Catalogue.Api.Mangas.Responses;
using MangaShelf.Catalogue.Application.Features.Mangas.Commands.CreateManga;
using MangaShelf.Catalogue.Application.Features.Mangas.Dtos;
using MangaShelf.Catalogue.Application.Features.Mangas.Queries.GetMangaById;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MangaShelf.Catalogue.Api.Mangas;

[ApiController]
public class MangaController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpPost("create-manga")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateManga([FromBody] CreateMangaRequest createManga,
        [FromServices] IValidator<CreateMangaRequest> validator,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAsync(createManga, ModelState, cancellationToken);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Result<Guid> id = await commandDispatcher.Dispatch<CreateMangaCommand, Result<Guid>>(
            new CreateMangaCommand(createManga.Name), cancellationToken
        );

        return Created($"/manga/{id.Value}", id.Value);
    }

    [HttpGet("manga/{id:guid}")]
    [ProducesResponseType(typeof(GetMangaByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMangaById([FromRoute]Guid id, CancellationToken cancellationToken)
    {
        MangaDto? mangaDto = await queryDispatcher.Dispatch<GetMangaByIdQuery, MangaDto?>(
            new GetMangaByIdQuery(id), cancellationToken
        );

        if (mangaDto is null)
            return NotFound();

        return Ok(new GetMangaByIdResponse{Id = mangaDto.Id, Name = mangaDto.Name});
    }
}