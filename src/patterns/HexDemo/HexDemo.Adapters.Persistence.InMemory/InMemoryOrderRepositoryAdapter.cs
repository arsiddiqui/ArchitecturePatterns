using System.Collections.Concurrent;
using HexDemo.Core.Domain;
using HexDemo.Core.Ports;

namespace HexDemo.Adapters.Persistence.InMemory;

public sealed class InMemoryOrderRepositoryAdapter : IOrderRepositoryPort
{
    private readonly ConcurrentDictionary<Guid, Order> _store = new();

    public Task AddAsync(Order order, CancellationToken ct)
    {
        _store[order.Id] = order;
        return Task.CompletedTask;
    }

    public Task<Order?> GetAsync(Guid id, CancellationToken ct)
        => Task.FromResult(_store.TryGetValue(id, out var o) ? o : null);

    public Task SaveAsync(Order order, CancellationToken ct)
    {
        _store[order.Id] = order;
        return Task.CompletedTask;
    }
}
