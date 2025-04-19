using FluentAssertions;
using MangaShelf.Domain.Abstractions.Tests.TestImplementations;

namespace MangaShelf.Domain.Abstractions.Tests;

public class EntityTests
{
    [Fact]
    public void GivenNullId_WhenCreatingNewEntity_ThenThrowsBusinessRuleException()
    {
        TestEntityId? testEntityId = null;

        Func<TestEntity> when = () => new TestEntity(testEntityId!);

        when.Should().Throw<BusinessRuleException>().WithMessage("Id may not be null.");
    }
}