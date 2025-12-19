using LayeredDemo.Application;
using LayeredDemo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/orders", async (OrderService svc, PlaceOrderRequest req, CancellationToken ct) =>
{
    var res = await svc.PlaceOrderAsync(req.CustomerId, req.Total, ct);
    return res.IsSuccess ? Results.Created($"/orders/{res.Value!.Id}", res.Value) : Results.BadRequest(new { res.Error });
});

app.MapPost("/orders/{id:guid}/pay", async (OrderService svc, Guid id, CancellationToken ct) =>
{
    var res = await svc.PayAsync(id, ct);
    return res.IsSuccess ? Results.Ok(res.Value) : Results.NotFound(new { res.Error });
});

app.Run();

public sealed record PlaceOrderRequest(string CustomerId, decimal Total);
