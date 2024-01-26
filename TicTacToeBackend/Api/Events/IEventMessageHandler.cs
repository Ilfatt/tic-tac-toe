using Api.Hubs.Abstractions;

namespace Api.Events;

public interface IEventMessageHandler : IGamesHub
{
    public new Task GameStarted(GameStartEvent @event);
}