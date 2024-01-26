using Api.Events;

namespace Api.Hubs.Abstractions;

public interface IGamesHub
{
    public new Task GameStarted(GameStartEvent @event);
}