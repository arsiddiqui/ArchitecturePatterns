using CqrsDemo.Contracts;
using CqrsDemo.Infrastructure;

namespace CqrsDemo.Queries;

public sealed class GetOrderHandler(ReadStore readStore)
{
    public Task<OrderReadDto?> HandleAsync(GetOrderQuery query, CancellationToken ct)
        => Task.FromResult(readStore.Orders.TryGetValue(query.OrderId, out var dto) ? dto : null);
}
