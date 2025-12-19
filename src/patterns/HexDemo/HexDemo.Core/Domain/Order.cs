namespace HexDemo.Core.Domain;

public enum OrderStatus { Created, Paid }

public sealed class Order
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string CustomerId { get; init; } = "";
    public decimal Total { get; init; }
    public OrderStatus Status { get; private set; } = OrderStatus.Created;

    public static Order Create(string customerId, decimal total)
        => new() { CustomerId = customerId, Total = total };

    public void Pay() => Status = OrderStatus.Paid;
}
