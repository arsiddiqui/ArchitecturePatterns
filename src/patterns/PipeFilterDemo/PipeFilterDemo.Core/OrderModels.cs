namespace PipeFilterDemo.Core;

public sealed record IncomingOrder(string CustomerId, decimal Subtotal, string? CouponCode);
public sealed record ProcessedOrder(string CustomerId, decimal Subtotal, decimal Total, List<string> Steps);
