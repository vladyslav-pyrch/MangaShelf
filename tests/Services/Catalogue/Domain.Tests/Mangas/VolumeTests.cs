using FluentAssertions;
using MangaShelf.Catalogue.Domain.Mangas;
using MangaShelf.Domain.Abstractions;
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

    [Fact]
    public void GivenTitleIsEmpty_WhenCreatingVolume_ThenThrowsBusinessRuleException()
    {
        var id = new VolumeId(Guid.NewGuid());
        var title = string.Empty;
        var order = 1;

        Action when = () => Volume.Create(id, title, order);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenTitleIsNull_WhenCreatingVolume_ThenThrowsBusinessRuleException()
    {
        var id = new VolumeId(Guid.NewGuid());
        string title = null!;
        var order = 1;

        Action when = () => Volume.Create(id, title, order);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenTitleIsLongerThan50Chars_WhenCreatingVolume_ThenThrowBusinessRuleException()
    {
        var id = new VolumeId(Guid.NewGuid());
        var title = new string('a', 51);
        var order = 1;

        Action when = () => Volume.Create(id, title, order);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenOrderIsZero_WhenCreatingVolume_ThenThrowsBusinessRuleException()
    {
        var id = new VolumeId(Guid.NewGuid());
        var title = "Volume 1";
        var order = 0;

        Action when = () => Volume.Create(id, title, order);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenOrderIsNegative_WhenCreatingVolume_ThenThrowsBusinessRuleException()
    {
        var id = new VolumeId(Guid.NewGuid());
        var title = "Volume 1";
        var order = -1;

        Action when = () => Volume.Create(id, title, order);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenNewTitle_WhenChangingTitle_ThenTitleIsChanged()
    {
        Volume volume = NewVolume();
        var newTitle = "New Volume Title";

        volume.ChangeTitle(newTitle);

        volume.Title.Should().BeEquivalentTo(newTitle);
    }

    [Fact]
    public void GivenNewTitleIsNull_WhenChangingTitle_ThenThrowBusinessRuleException()
    {
        Volume volume = NewVolume();
        string newTitle = null!;

        Action when = () => volume.ChangeTitle(newTitle);

        when.Should().Throw<BusinessRuleException>();
    }

    [Fact]
    public void GivenNewTitleIsEmpty_WhenChangingTitle_ThenThrowBusinessRuleException()
    {
        Volume volume = NewVolume();
        string newTitle = string.Empty;

        Action when = () => volume.ChangeTitle(newTitle);

        when.Should().Throw<BusinessRuleException>();
    }

    private Volume NewVolume()
    {
        var id = new VolumeId(Guid.NewGuid());
        var title = "Volume 1";
        var order = 1;

        return Volume.Create(id, title, order);
    }
}