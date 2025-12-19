namespace PipeFilterDemo.Core;

public sealed class ValidateFilter : IFilter<IncomingOrder>
{
    public Task<IncomingOrder> ExecuteAsync(IncomingOrder input, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(input.CustomerId))
            throw new ArgumentException("CustomerId is required.");
        if (input.Subtotal <= 0)
            throw new ArgumentException("Subtotal must be > 0.");
        return Task.FromResult(input);
    }
}

public sealed class EnrichFilter : IFilter<IncomingOrder>
{
    public Task<IncomingOrder> ExecuteAsync(IncomingOrder input, CancellationToken ct)
    {
        // demo: normalize coupon
        var coupon = string.IsNullOrWhiteSpace(input.CouponCode) ? null : input.CouponCode!.Trim().ToUpperInvariant();
        return Task.FromResult(input with { CouponCode = coupon });
    }
}

public sealed class PriceFilter : IFilter<IncomingOrder>
{
    public Task<IncomingOrder> ExecuteAsync(IncomingOrder input, CancellationToken ct)
        => Task.FromResult(input);
}

public static class OrderPipeline
{
    public static async Task<ProcessedOrder> RunAsync(IncomingOrder input, CancellationToken ct)
    {
        var steps = new List<string>();
        var pipeline = new Pipeline<IncomingOrder>(new IFilter<IncomingOrder>[]
        {
            new ValidateFilter(),
            new EnrichFilter(),
            new PriceFilter()
        });

        var enriched = await pipeline.ExecuteAsync(input, ct);
        steps.Add("Validated");
        steps.Add("Enriched");

        var total = enriched.Subtotal;
        if (enriched.CouponCode == "SAVE10") total = Math.Round(total * 0.90m, 2);

        steps.Add("Priced");
        return new ProcessedOrder(enriched.CustomerId, enriched.Subtotal, total, steps);
    }
}
