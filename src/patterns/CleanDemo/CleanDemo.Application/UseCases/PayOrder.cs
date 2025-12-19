using CleanDemo.Application.Abstractions;
using SharedKernel;

namespace CleanDemo.Application.UseCases;

public sealed class PayOrder(IOrderRepository repo)
{
    public async Task<Result<object>> HandleAsync(Guid orderId, CancellationToken ct)
    {
        var order = await repo.GetAsync(orderId, ct);
        if (order is null) return Result<object>.Fail("Order not found.");
        try { order.Pay(); } catch (Exception ex) { return Result<object>.Fail(ex.Message); }
        await repo.SaveAsync(order, ct);
        return Result<object>.Ok(new { order.Id, order.Status });
    }
}
