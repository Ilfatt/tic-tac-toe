using Api.Hubs.Abstractions;

namespace Api.Events;

public interface IEventMessageHandler : IGamesHub
{
    public new Task GameStarted(GameStartEvent @event);
    public new Task MoveMade(GameMoveEvent @event);
    public new Task GameFinish(GameFinishedEvent @event);
}