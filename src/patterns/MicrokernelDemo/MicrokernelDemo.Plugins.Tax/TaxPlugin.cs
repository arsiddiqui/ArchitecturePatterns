using MicrokernelDemo.Abstractions;

namespace MicrokernelDemo.Plugins.Tax;

// Demo rule: apply 7.5% tax for everyone.
public sealed class TaxPlugin : IOrderPricingPlugin
{
    public string Name => "Sales Tax 7.5%";

    public decimal Apply(PricingContext ctx, decimal currentTotal)
        => Math.Round(currentTotal * 1.075m, 2);
}
