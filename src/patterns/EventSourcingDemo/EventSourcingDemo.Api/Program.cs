using EventSourcingDemo.Core;
using EventSourcingDemo.EventStore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<InMemoryEventStore>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

// Create order (writes OrderCreated event)
app.MapPost("/orders", async (InMemoryEventStore store, CreateOrderRequest req, CancellationToken ct) =>
{
    var orderId = Guid.NewGuid();
    var agg = new OrderAggregate();
    var events = agg.Create(orderId, req.CustomerId).ToList();
    await store.AppendAsync(orderId, events, ct);
    return Results.Created($"/orders/{orderId}", new { orderId });
});

// Add item (writes ItemAdded event)
app.MapPost("/orders/{id:guid}/items", async (InMemoryEventStore store, Guid id, AddItemRequest req, CancellationToken ct) =>
{
    var history = await store.LoadAsync(id, ct);
    if (history.Count == 0) return Results.NotFound();

    var agg = OrderAggregate.FromHistory(history);
    var newEvents = agg.AddItem(req.Sku, req.Qty, req.UnitPrice).ToList();
    foreach (var e in newEvents) agg.Apply(e);

    await store.AppendAsync(id, newEvents, ct);
    return Results.Ok(new { agg.Id, agg.CustomerId, agg.Total, agg.Items });
});

// Read current state by replaying events
app.MapGet("/orders/{id:guid}", async (InMemoryEventStore store, Guid id, CancellationToken ct) =>
{
    var history = await store.LoadAsync(id, ct);
    if (history.Count == 0) return Results.NotFound();

    var agg = OrderAggregate.FromHistory(history);
    return Results.Ok(new { agg.Id, agg.CustomerId, agg.Total, agg.Items });
});

// Get raw event stream
app.MapGet("/orders/{id:guid}/events", async (InMemoryEventStore store, Guid id, CancellationToken ct) =>
{
    var history = await store.LoadAsync(id, ct);
    return history.Count == 0 ? Results.NotFound() : Results.Ok(history);
});

app.Run();

public sealed record CreateOrderRequest(string CustomerId);
public sealed record AddItemRequest(string Sku, int Qty, decimal UnitPrice);
