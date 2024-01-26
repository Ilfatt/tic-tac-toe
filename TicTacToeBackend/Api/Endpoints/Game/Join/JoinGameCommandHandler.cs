using Api.Events;
using Core.Contracts;
using Core.Features.Game;
using Core.Features.Game.Join;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using TemporaryStorage;
using TemporaryStorage.Models;

namespace Api.Endpoints.Game.Join;

public class JoinGameCommandHandler(
		IDbContext dbContext,
		IEventMessageHandler eventMessageHandler,
		IMongoDbStorage<UserRating> mongoDbStorage)
	: ICommandHandler<JoinGameCommand, JoinGameResult>
{
	public async Task<Result<JoinGameResult>> Handle(JoinGameCommand request, CancellationToken cancellationToken)
	{
		var game = await dbContext.Games.FirstOrDefaultAsync(g => g.Id == request.GameId,
			cancellationToken: cancellationToken);
		if (game is null) return 400;
		if (game.OpponentId == game.OwnerId) return 409;
		
		if (game.OpponentId is null)
		{
			var userRating = await mongoDbStorage.GetByIdAsync(request.UserId);
			if (userRating!.Rating < game.MinRate || userRating.Rating > game.MaxRate)
				return 400;

			game.OpponentId = request.UserId;
			game.GameState = GameState.Started;

			var userGames = dbContext.Games
				.Where(x => x.OpponentId == request.UserId || x.OwnerId == request.UserId);

			foreach (var userGame in userGames)
			{
				if (userGame.OpponentId == request.UserId) userGame.OpponentId = null;
				if (userGame.OwnerId == request.UserId) dbContext.Games.Remove(userGame);
			}
			
			await eventMessageHandler.GameStarted(new GameStartEvent(game.Id, game.OwnerId, game.OpponentId.Value, game.OpponentId.Value));
			await dbContext.SaveChangesAsync(cancellationToken);
		}

		return new JoinGameResult(game.Id, game.OwnerId, game.OpponentId, new GameProgress
		{
			GameMap = game.GameMap,
			MoveUserId = game.OpponentId
		});
	}
}