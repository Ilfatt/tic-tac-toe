using Api.Hubs.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

[Authorize]
public class GamesHub() : Hub<IGamesHub>
{
    public async Task Join(Guid gameId)
    {
       await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
    }
}