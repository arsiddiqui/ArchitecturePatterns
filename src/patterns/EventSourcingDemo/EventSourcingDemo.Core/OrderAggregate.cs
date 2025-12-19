namespace EventSourcingDemo.Core;

public sealed class OrderAggregate
{
    public Guid Id { get; private set; }
    public string CustomerId { get; private set; } = "";
    public decimal Total { get; private set; }
    public List<(string Sku, int Qty, decimal UnitPrice)> Items { get; } = new();

    public static OrderAggregate FromHistory(IEnumerable<IEvent> events)
    {
        var agg = new OrderAggregate();
        foreach (var e in events) agg.Apply(e);
        return agg;
    }

    public IEnumerable<IEvent> Create(Guid orderId, string customerId)
    {
        yield return new OrderCreated(orderId, customerId);
    }

    public IEnumerable<IEvent> AddItem(string sku, int qty, decimal unitPrice)
    {
        if (Id == Guid.Empty) throw new InvalidOperationException("Order not created.");
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        if (unitPrice < 0) throw new ArgumentOutOfRangeException(nameof(unitPrice));
        yield return new ItemAdded(Id, sku, qty, unitPrice);
    }

    public void Apply(IEvent e)
    {
        switch (e)
        {
            case OrderCreated oc:
                Id = oc.OrderId;
                CustomerId = oc.CustomerId;
                break;
            case ItemAdded ia:
                Items.Add((ia.Sku, ia.Qty, ia.UnitPrice));
                Total += ia.Qty * ia.UnitPrice;
                break;
            default:
                throw new NotSupportedException($"Unknown event {e.GetType().Name}");
        }
    }
}
