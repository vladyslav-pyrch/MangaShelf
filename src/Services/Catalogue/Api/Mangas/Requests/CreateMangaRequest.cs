using System.ComponentModel.DataAnnotations;

namespace MangaShelf.Catalogue.Api.Mangas.Requests;

public class CreateMangaRequest
{
    public string? Name { get; set; } = null!;
}