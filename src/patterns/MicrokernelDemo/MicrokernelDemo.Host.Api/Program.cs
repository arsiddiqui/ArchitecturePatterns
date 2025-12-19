using MicrokernelDemo.Abstractions;
using MicrokernelDemo.Plugins.Discounts;
using MicrokernelDemo.Plugins.Tax;

var builder = WebApplication.CreateBuilder(args);

// Microkernel: core host + plugins. Here we register plugins via DI (simple).
builder.Services.AddSingleton<IOrderPricingPlugin, DiscountPlugin>();
builder.Services.AddSingleton<IOrderPricingPlugin, TaxPlugin>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/price", (IEnumerable<IOrderPricingPlugin> plugins, PriceRequest req) =>
{
    var ctx = new PricingContext(req.CustomerId, req.Subtotal);
    var total = req.Subtotal;

    var applied = new List<object>();
    foreach (var p in plugins)
    {
        var before = total;
        total = p.Apply(ctx, total);
        applied.Add(new { plugin = p.Name, before, after = total });
    }

    return Results.Ok(new { req.CustomerId, req.Subtotal, total, applied });
});

app.Run();

public sealed record PriceRequest(string CustomerId, decimal Subtotal);
