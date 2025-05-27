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
        private set
        {
            BusinessRuleException.ThrowIfNullOrWhiteSpace(value, $"{nameof(Title)} may not be null or whitespace.");

            if (value.Length > 50)
                throw new BusinessRuleException($"{nameof(Title)} may not be longer than 50 characters.");

            _title = value;
        }
    }

    public int Order
    {
        get => _order;
        private set
        {
            if (value <= 0)
                throw new BusinessRuleException($"{nameof(Order)} may not be zero or negative.");

            _order = value;
        }
    }

    public static Volume Create(VolumeId id, string title, int order) => new(id, title, order);

    public void ChangeTitle(string title) => Title = title;
}