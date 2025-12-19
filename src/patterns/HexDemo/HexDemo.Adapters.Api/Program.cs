using HexDemo.Adapters.Persistence.InMemory;
using HexDemo.Core.Ports;
using HexDemo.Core.UseCases;

var builder = WebApplication.CreateBuilder(args);

// In Hexagonal: API and Persistence are adapters plugged into Core ports.
builder.Services.AddSingleton<IOrderRepositoryPort, InMemoryOrderRepositoryAdapter>();
builder.Services.AddScoped<PlaceOrderUseCase>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/orders", async (PlaceOrderUseCase uc, PlaceOrderRequest req, CancellationToken ct) =>
{
    var res = await uc.HandleAsync(req.CustomerId, req.Total, ct);
    return res.IsSuccess ? Results.Created($"/orders/{res.Value!.Id}", res.Value) : Results.BadRequest(new { res.Error });
});

app.Run();

public sealed record PlaceOrderRequest(string CustomerId, decimal Total);
