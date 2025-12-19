using CqrsDemo.Commands;
using CqrsDemo.Contracts;
using CqrsDemo.Infrastructure;
using CqrsDemo.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<WriteStore>();
builder.Services.AddSingleton<ReadStore>();

builder.Services.AddScoped<PlaceOrderHandler>();
builder.Services.AddScoped<GetOrderHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/commands/orders", async (PlaceOrderHandler h, PlaceOrderCommand cmd, CancellationToken ct) =>
{
    var res = await h.HandleAsync(cmd, ct);
    return Results.Created($"/queries/orders/{res.OrderId}", res);
});

app.MapGet("/queries/orders/{id:guid}", async (GetOrderHandler h, Guid id, CancellationToken ct) =>
{
    var dto = await h.HandleAsync(new GetOrderQuery(id), ct);
    return dto is null ? Results.NotFound() : Results.Ok(dto);
});

app.Run();
