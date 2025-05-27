using FluentAssertions;
using MangaShelf.Catalogue.Domain.Mangas;
using Xunit.Abstractions;

namespace MangaShelf.Catalogue.Domain.Tests.Mangas;

public class VolumeTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void GivenIdAndTitleAndOrder_WhenCreatingVolume_ThenVolumeIsCreated()
    {
        var id = new VolumeId(Guid.NewGuid());
        var title = "Volume 1";
        var order = 1;

        var volume = Volume.Create(id, title, order);

        volume.Id.Should().Be(id);
        volume.Title.Should().BeEquivalentTo(title);
        volume.Order.Should().Be(1);
    }
}