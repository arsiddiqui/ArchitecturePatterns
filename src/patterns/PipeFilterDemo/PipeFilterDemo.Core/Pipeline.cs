namespace PipeFilterDemo.Core;

public interface IFilter<T>
{
    Task<T> ExecuteAsync(T input, CancellationToken ct);
}

public sealed class Pipeline<T>(IEnumerable<IFilter<T>> filters)
{
    private readonly IReadOnlyList<IFilter<T>> _filters = filters.ToList();

    public async Task<T> ExecuteAsync(T input, CancellationToken ct)
    {
        var current = input;
        foreach (var f in _filters)
            current = await f.ExecuteAsync(current, ct);
        return current;
    }
}
