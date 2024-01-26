using Api.Events;
using Api.Services.Abstractions;
using Core.Contracts;
using Core.Features.Game;
using Core.Features.Game.Join;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using TemporaryStorage;
using TemporaryStorage.Models;

namespace Api.Endpoints.Game.Join;

public class JoinGameCommandHandler(
    [FromServices] IUserService userService,
    [FromServices] IDbContext dbContext,
    [FromServices] IEventMessageHandler eventMessageHandler,
    [FromServices] IMongoDbStorage<UserRating> mongoDbStorage)
    : ICommandHandler<JoinGameCommand, JoinGameResult>
{
    public async Task<Result<JoinGameResult>> Handle(JoinGameCommand request, CancellationToken cancellationToken)
    {
        var game = await dbContext.Games.FirstOrDefaultAsync(g => g.Id == request.GameId,
            cancellationToken: cancellationToken);
        if (game is null)
            return 400;

        var userId = userService.GetUserIdOrDefault();
        if (userId is null)
            return 400;

        if (game.OpponentId is null)
        {
            var userRating = await mongoDbStorage.GetByIdAsync(userId.Value);
            if (userRating!.Rating < game.MaxRate || userRating.Rating > game.MaxRate)
                return 400;
            
            game.OpponentId = userId;
        }
        
        game.GameState = GameState.Started;

        await eventMessageHandler.GameStarted(new GameStartEvent(game.Id, game.OwnerId, game.OpponentId.Value));

        await dbContext.SaveChangesAsync(cancellationToken);

        return new JoinGameResult(game.Id, game.OwnerId, game.OpponentId, new GameProgress
        {
            GameMap = game.GameMap,
            MoveUserId = game.OpponentId
        });
    }
}