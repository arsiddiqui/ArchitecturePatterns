using System.Collections.Concurrent;
using LayeredDemo.Application;
using LayeredDemo.Domain;

namespace LayeredDemo.Infrastructure;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<Guid, Order> _store = new();

    public Task<Order?> GetAsync(Guid id, CancellationToken ct)
        => Task.FromResult(_store.TryGetValue(id, out var o) ? o : null);

    public Task AddAsync(Order order, CancellationToken ct)
    {
        _store[order.Id] = order;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Order order, CancellationToken ct)
    {
        _store[order.Id] = order;
        return Task.CompletedTask;
    }
}
