namespace LayeredDemo.Domain;

public enum OrderStatus { Created, Paid, Cancelled }

public sealed class Order
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string CustomerId { get; init; } = "";
    public decimal Total { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Created;

    public static Order Create(string customerId, decimal total)
        => new() { CustomerId = customerId, Total = total, Status = OrderStatus.Created };

    public void MarkPaid()
    {
        if (Status is OrderStatus.Cancelled) throw new InvalidOperationException("Order cancelled.");
        Status = OrderStatus.Paid;
    }

    public void Cancel()
    {
        if (Status is OrderStatus.Paid) throw new InvalidOperationException("Paid order cannot be cancelled in this demo.");
        Status = OrderStatus.Cancelled;
    }
}
