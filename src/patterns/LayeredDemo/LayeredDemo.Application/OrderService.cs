using LayeredDemo.Domain;
using SharedKernel;

namespace LayeredDemo.Application;

public sealed class OrderService(IOrderRepository repo)
{
    public async Task<Result<Order>> PlaceOrderAsync(string customerId, decimal total, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(customerId)) return Result<Order>.Fail("customerId required.");
        if (total <= 0) return Result<Order>.Fail("total must be > 0.");

        var order = Order.Create(customerId, total);
        await repo.AddAsync(order, ct);
        return Result<Order>.Ok(order);
    }

    public async Task<Result<Order>> PayAsync(Guid orderId, CancellationToken ct)
    {
        var order = await repo.GetAsync(orderId, ct);
        if (order is null) return Result<Order>.Fail("Order not found.");

        try { order.MarkPaid(); }
        catch (Exception ex) { return Result<Order>.Fail(ex.Message); }

        await repo.UpdateAsync(order, ct);
        return Result<Order>.Ok(order);
    }
}
