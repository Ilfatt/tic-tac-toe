using Core.Contracts;
using Core.Features.Game;
using Core.Features.Game.GetAll;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Endpoints.Game.GetAll;

public class GetAllGamesCommandHandler(IDbContext dbContext) : IQueryHandler<GetAllGamesQuery, GetAllGamesResult>
{
    public async Task<Result<GetAllGamesResult>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await dbContext.Games.CountAsync(cancellationToken);
        var gameResults = await dbContext.Games
            .Skip((request.Page - 1) * request.Size)
            .Take(request.Size)
            .Select(game =>
            new GetGameResult(game.Id, new RateRange
            {
                Min = game.MinRate,
                Max = game.MaxRate,
            }, game.GameState))
            .ToArrayAsync(cancellationToken: cancellationToken);

        return new GetAllGamesResult(gameResults, totalCount);
    }
}