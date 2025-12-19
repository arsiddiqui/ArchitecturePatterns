namespace CleanDemo.Domain;

public enum OrderStatus { Created, Paid, Cancelled }

public sealed class Order
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string CustomerId { get; init; } = "";
    public decimal Total { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Created;

    public static Order Create(string customerId, decimal total)
        => new() { CustomerId = customerId, Total = total, Status = OrderStatus.Created };

    public void Pay()
    {
        if (Status == OrderStatus.Cancelled) throw new InvalidOperationException("Cannot pay cancelled order.");
        Status = OrderStatus.Paid;
    }
}
