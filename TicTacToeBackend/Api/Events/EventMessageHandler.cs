using Api.Hubs;
using Api.Hubs.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Api.Events;

public class GameEventMessageHandler(IHubContext<GamesHub, IGamesHub> hubContext) : IEventMessageHandler
{
    public Task MoveMade()
    {
        throw new NotImplementedException();
    }

    public async Task GameStarted(GameStartEvent @event)
    {
        await hubContext.Clients.Group(@event.GameId.ToString()).GameStarted(@event);
    }
    
}