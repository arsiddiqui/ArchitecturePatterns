using HexDemo.Core.Domain;

namespace HexDemo.Core.Ports;

public interface IOrderRepositoryPort
{
    Task AddAsync(Order order, CancellationToken ct);
    Task<Order?> GetAsync(Guid id, CancellationToken ct);
    Task SaveAsync(Order order, CancellationToken ct);
}
