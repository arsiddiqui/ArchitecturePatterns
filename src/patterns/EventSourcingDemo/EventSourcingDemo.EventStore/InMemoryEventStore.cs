using System.Collections.Concurrent;
using EventSourcingDemo.Core;

namespace EventSourcingDemo.EventStore;

public sealed class InMemoryEventStore
{
    private readonly ConcurrentDictionary<Guid, List<IEvent>> _streams = new();

    public Task AppendAsync(Guid streamId, IEnumerable<IEvent> events, CancellationToken ct)
    {
        var list = _streams.GetOrAdd(streamId, _ => new List<IEvent>());
        list.AddRange(events);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<IEvent>> LoadAsync(Guid streamId, CancellationToken ct)
        => Task.FromResult((IReadOnlyList<IEvent>)(_streams.TryGetValue(streamId, out var list) ? list : new List<IEvent>()));
}
