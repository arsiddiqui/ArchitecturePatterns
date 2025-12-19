using MicrokernelDemo.Abstractions;

namespace MicrokernelDemo.Plugins.Discounts;

// Demo rule: VIP customers get 10% off.
public sealed class DiscountPlugin : IOrderPricingPlugin
{
    public string Name => "VIP 10% Discount";

    public decimal Apply(PricingContext ctx, decimal currentTotal)
        => ctx.CustomerId.StartsWith("VIP-", StringComparison.OrdinalIgnoreCase)
            ? Math.Round(currentTotal * 0.90m, 2)
            : currentTotal;
}
