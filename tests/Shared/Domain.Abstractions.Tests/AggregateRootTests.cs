using FluentAssertions;
using MangaShelf.Domain.Abstractions.Tests.TestImplementations;

namespace MangaShelf.Domain.Abstractions.Tests;

public class AggregateRootTests
{
    [Fact]
    public void GivenNewAggregate_WhenRetrievingDomainEvents()
    {
        TestAggregate aggregate = new TestAggregate();

        ThenDomainEventsIsEmpty(aggregate.DomainEvents);
    }

    private void ThenDomainEventsIsEmpty(IReadOnlyCollection<IDomainEvent> domainEvents)
    {
        domainEvents.Should().BeEmpty();
    }

    [Fact]
    public void GivenAggregateWithSomeEvents_WhenRetrievingDomainEvents()
    {
        TestAggregate aggregate = new TestAggregate();
        aggregate.SomeChange();

        ThenDomainEventsHasAnEvent(aggregate.DomainEvents);
    }

    private void ThenDomainEventsHasAnEvent(IReadOnlyCollection<IDomainEvent> domainEvents)
    {
        domainEvents.Should().Equal(new TestDomainEvent());
    }

    [Fact]
    public void GivenAggregateWithSomeEvents_WhenClearingTheEvents()
    {
        TestAggregate aggregate = new TestAggregate();
        aggregate.SomeChange();

        ThenDomainEventsHasAnEvent(aggregate.DomainEvents);

        aggregate.ClearDomainEvents();

        ThenDomainEventsIsEmpty(aggregate.DomainEvents);
    }
}