using FluentValidation;
using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Api.Extensions;
using MangaShelf.Catalogue.Api.Mangas.Requests;
using MangaShelf.Catalogue.Api.Mangas.Responses;
using MangaShelf.Catalogue.Application.Features.Mangas.Commands.ChangeDescription;
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
            new CreateMangaCommand(createManga.Title, createManga.AuthorId), cancellationToken
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

        return Ok(new GetMangaByIdResponse
        {
            Id = mangaDto.Id,
            Title = mangaDto.Name,
            AuthorId = mangaDto.AuthorId,
            Description = mangaDto.Description
        });
    }

    [HttpPut("manga/{id:guid}/change-description")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeDescription([FromRoute] Guid id, [FromBody] ChangeDescriptionRequest request,
        IValidator<ChangeDescriptionRequest> validator, CancellationToken cancellationToken)
    {
        await validator.ValidateAsync(request, ModelState, cancellationToken);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Result<Unit> _ = await commandDispatcher.Dispatch<ChangeDescriptionCommand, Result<Unit>>(
            new ChangeDescriptionCommand(id, request.Description), cancellationToken
        );

        return NoContent();
    }

    [HttpPost("manga/{id:guid}/add-volume")]
    [ProducesResponseType(typeof(Guid),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddVolume([FromRoute] Guid id, [FromBody] AddVolumeRequest addVolume,
        IValidator<AddVolumeRequest> validator, CancellationToken cancellationToken)
    {
        await validator.ValidateAsync(addVolume, ModelState, cancellationToken);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var volumeId = Guid.CreateVersion7();

        return Created($"/manga/{id}/volume/{Guid.CreateVersion7()}", volumeId);
    }

}