using CqrsDemo.Contracts;
using CqrsDemo.Infrastructure;

namespace CqrsDemo.Commands;

public sealed class PlaceOrderHandler(WriteStore writeStore, ReadStore readStore)
{
    public Task<OrderCreatedResponse> HandleAsync(PlaceOrderCommand cmd, CancellationToken ct)
    {
        var id = Guid.NewGuid();
        writeStore.Orders[id] = (cmd.CustomerId, cmd.Total, "Created");

        // "Projection" to read model (in real systems: async event handlers)
        readStore.Orders[id] = new OrderReadDto(id, cmd.CustomerId, cmd.Total, "Created");

        return Task.FromResult(new OrderCreatedResponse(id));
    }
}
