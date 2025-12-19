using CleanDemo.Domain;

namespace CleanDemo.Application.Abstractions;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken ct);
    Task<Order?> GetAsync(Guid id, CancellationToken ct);
    Task SaveAsync(Order order, CancellationToken ct);
}
