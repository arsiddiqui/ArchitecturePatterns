using CleanDemo.Application.Abstractions;
using CleanDemo.Application.UseCases;
using CleanDemo.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Outer layers depend on inner layers; Infrastructure implements Application abstractions.
builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddScoped<PlaceOrder>();
builder.Services.AddScoped<PayOrder>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/orders", async (PlaceOrder uc, PlaceOrderRequest req, CancellationToken ct) =>
{
    var res = await uc.HandleAsync(req.CustomerId, req.Total, ct);
    return res.IsSuccess ? Results.Created($"/orders/{res.Value!.Id}", res.Value) : Results.BadRequest(new { res.Error });
});

app.MapPost("/orders/{id:guid}/pay", async (PayOrder uc, Guid id, CancellationToken ct) =>
{
    var res = await uc.HandleAsync(id, ct);
    return res.IsSuccess ? Results.Ok(res.Value) : Results.NotFound(new { res.Error });
});

app.Run();

public sealed record PlaceOrderRequest(string CustomerId, decimal Total);
