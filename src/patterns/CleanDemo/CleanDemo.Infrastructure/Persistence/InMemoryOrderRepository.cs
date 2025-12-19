using System.Collections.Concurrent;
using CleanDemo.Application.Abstractions;
using CleanDemo.Domain;

namespace CleanDemo.Infrastructure.Persistence;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<Guid, Order> _orders = new();

    public Task AddAsync(Order order, CancellationToken ct)
    {
        _orders[order.Id] = order;
        return Task.CompletedTask;
    }

    public Task<Order?> GetAsync(Guid id, CancellationToken ct)
        => Task.FromResult(_orders.TryGetValue(id, out var o) ? o : null);

    public Task SaveAsync(Order order, CancellationToken ct)
    {
        _orders[order.Id] = order;
        return Task.CompletedTask;
    }
}
