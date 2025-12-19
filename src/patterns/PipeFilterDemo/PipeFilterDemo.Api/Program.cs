using PipeFilterDemo.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/pipeline/orders", async (IncomingOrder req, CancellationToken ct) =>
{
    try
    {
        var result = await OrderPipeline.RunAsync(req, ct);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.Run();
