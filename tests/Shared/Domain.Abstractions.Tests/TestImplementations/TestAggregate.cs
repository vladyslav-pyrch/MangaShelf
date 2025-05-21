namespace MangaShelf.Domain.Abstractions.Tests.TestImplementations;

public class TestAggregate() : AggregateRoot<TestAggregateId>(new TestAggregateId())
{
    public void SomeChange()
    {
        RaiseDomainEvent(new TestDomainEvent());
    }
}