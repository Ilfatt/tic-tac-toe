using Api.Events;

namespace Api.Hubs.Abstractions;

public interface IGamesHub
{
    public Task GameStarted(GameStartEvent @event);
    public Task MoveMade(GameMoveEvent @event);
    
    public Task GameFinish(GameFinishEvent @event);

}