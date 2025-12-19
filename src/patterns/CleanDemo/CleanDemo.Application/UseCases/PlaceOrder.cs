using CleanDemo.Application.Abstractions;
using CleanDemo.Domain;
using SharedKernel;

namespace CleanDemo.Application.UseCases;

public sealed class PlaceOrder(IOrderRepository repo)
{
    public async Task<Result<Order>> HandleAsync(string customerId, decimal total, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(customerId)) return Result<Order>.Fail("customerId required.");
        if (total <= 0) return Result<Order>.Fail("total must be > 0.");
        var order = Order.Create(customerId, total);
        await repo.AddAsync(order, ct);
        return Result<Order>.Ok(order);
    }
}
