namespace MicrokernelDemo.Abstractions;

public sealed record PricingContext(string CustomerId, decimal Subtotal);

public interface IOrderPricingPlugin
{
    string Name { get; }
    decimal Apply(PricingContext ctx, decimal currentTotal);
}
