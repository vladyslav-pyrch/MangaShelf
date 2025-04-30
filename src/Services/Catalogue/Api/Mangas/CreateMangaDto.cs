using System.ComponentModel.DataAnnotations;

namespace MangaShelf.Catalogue.Api.Mangas;

public class CreateMangaDto
{
    [Required] public string Name { get; set; } = null!;
}