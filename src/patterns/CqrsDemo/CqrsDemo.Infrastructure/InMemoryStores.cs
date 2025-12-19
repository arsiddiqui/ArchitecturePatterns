using System.Collections.Concurrent;
using CqrsDemo.Contracts;

namespace CqrsDemo.Infrastructure;

// In CQRS, writes and reads can use different models/stores.
// Here we keep two in-memory stores to demonstrate the separation.
public sealed class WriteStore
{
    public ConcurrentDictionary<Guid, (string CustomerId, decimal Total, string Status)> Orders { get; } = new();
}

public sealed class ReadStore
{
    public ConcurrentDictionary<Guid, OrderReadDto> Orders { get; } = new();
}
