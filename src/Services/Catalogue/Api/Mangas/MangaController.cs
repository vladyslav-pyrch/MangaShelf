using Microsoft.AspNetCore.Mvc;

namespace MangaShelf.Catalogue.Api.Mangas;

[ApiController]
public class MangaController : ControllerBase
{
    [HttpPost("create-manga")]
    public async Task<IActionResult> CreateManga([FromBody] CreateMangaDto createManga)
    {
        var id = Guid.NewGuid();

        return Created($"/manga/{id}", new
        {
            id,
            createManga.Name
        });
    }
}