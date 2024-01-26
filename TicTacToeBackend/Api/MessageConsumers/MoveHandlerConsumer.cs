using Api.Events;
using Core.Contracts;
using Core.Extensions;
using Core.Features.Game;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Dto;
using TemporaryStorage;
using TemporaryStorage.Models;

namespace Api.MessageConsumers;

/// <summary>
/// Обрабочик хода
/// </summary>
public class MoveHandlerConsumer
	(IDbContext dbContext, IEventMessageHandler eventMessageHandler, IMongoDbStorage<UserRating> mongoDbStorage)
	: IConsumer<MoveDto>
{
	public async Task Consume(ConsumeContext<MoveDto> context)
	{
		var game = await dbContext.Games
			.FirstOrDefaultAsync(x =>
				(x.OpponentId == context.Message.UserId || context.Message.UserId == x.OwnerId)
				&& x.GameState != GameState.Finished);

		if (game is null) return;

		var isOwner = game.OwnerId == context.Message.UserId;
		var movesCount = game!.GameMap.Count(x => x != GameMapSymbol.Empty);
		var thisUserMovable = movesCount % 2 == 0 && isOwner || movesCount % 1 == 0 && !isOwner;

		if (!thisUserMovable) return;
		if (game.GameMap[context.Message.Index] != GameMapSymbol.Empty) return;

		game.GameMap[context.Message.Index] = isOwner ? GameMapSymbol.Circle : GameMapSymbol.Cross;

		var result = game.GameMap.CheckMap();

		if (result != GameMapSymbol.Empty)
		{
			await eventMessageHandler.GameFinish(
				new GameFinishEvent(
					game.Id,
					game.GameMap,
					result is null
						? null
						: context.Message.UserId));

			game.GameState = GameState.Finished;

			await dbContext.SaveChangesAsync();

			var winnerRating = await mongoDbStorage.GetByIdAsync(context.Message.UserId)
			                   ?? throw new ArgumentException(
				                   $"User rating for user with id: {context.Message.UserId} not found");
			var loserRating = await mongoDbStorage.GetByIdAsync(game.OpponentId!.Value)
			                  ?? throw new ArgumentException(
				                  $"User rating for user with id: {context.Message.UserId} not found");

			winnerRating.Rating += result is null ? -1 : 3;
			loserRating.Rating -= -1;

			await mongoDbStorage.UpdateAsync(winnerRating);
			await mongoDbStorage.UpdateAsync(loserRating);

			return;
		}

		var nextTurnUser = isOwner ? game.OwnerId : game.OpponentId;

		await eventMessageHandler.MoveMade(new GameMoveEvent(game.Id, game.GameMap, nextTurnUser!.Value));

		await dbContext.SaveChangesAsync();
	}
}