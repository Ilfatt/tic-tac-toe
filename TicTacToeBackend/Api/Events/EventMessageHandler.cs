using Api.Hubs;
using Api.Hubs.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Api.Events;

public class GameEventMessageHandler(IHubContext<GamesHub, IGamesHub> hubContext) : IEventMessageHandler
{
    public async Task GameStarted(GameStartEvent @event)
    {
        await hubContext.Clients.Group(@event.GameId.ToString()).GameStarted(@event);
    }
    
    public async Task MoveMade(GameMoveEvent @event)
    {
        await hubContext.Clients.Group(@event.GameId.ToString()).MoveMade(@event);
    }

    public async Task GameFinish(GameFinishEvent @event)
    {
        await hubContext.Clients.Group(@event.GameId.ToString()).GameFinish(@event);
    }
}