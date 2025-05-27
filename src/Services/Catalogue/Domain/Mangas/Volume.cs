using MangaShelf.Domain.Abstractions;

namespace MangaShelf.Catalogue.Domain.Mangas;

public class Volume : Entity<VolumeId>
{
    private string _title = null!;

    private int _order;

    public Volume(VolumeId id, string title, int order) : base(id)
    {
        Title = title;
        Order = order;
    }

    public string Title
    {
        get => _title;
        private set => _title = value;
    }

    public int Order
    {
        get => _order;
        private set => _order = value;
    }

    public static Volume Create(VolumeId id, string title, int order) => new(id, title, order);
}