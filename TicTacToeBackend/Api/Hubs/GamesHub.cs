using Api.Endpoints.Game.Disconnect;
using Api.Extensions;
using Api.Hubs.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

[Authorize]
public class GamesHub(IHttpContextAccessor contextAccessor, IMediator mediator) : Hub<IGamesHub>
{
    public async Task Join(Guid gameId)
    {
       await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = contextAccessor.HttpContext?.GetUserId();
        await mediator.Send(new DisconnectCommand(userId!.Value));
    }
}