namespace EventSourcingDemo.Core;

public interface IEvent
{
    DateTimeOffset OccurredAt { get; }
}

public sealed record OrderCreated(Guid OrderId, string CustomerId) : IEvent
{
    public DateTimeOffset OccurredAt { get; init; } = DateTimeOffset.UtcNow;
}

public sealed record ItemAdded(Guid OrderId, string Sku, int Qty, decimal UnitPrice) : IEvent
{
    public DateTimeOffset OccurredAt { get; init; } = DateTimeOffset.UtcNow;
}
