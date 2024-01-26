using Core.Contracts;
using Core.Features.Game;
using Core.Features.Game.Create;
using MediatR;
using Model;

namespace Api.Endpoints.Game.Create;

public class CreateGameCommandHandler(
    IDbContext domainContext)
    : ICommandHandler<CreateGameCommand, CreateGameResult>
{ 
    public async Task<Result<CreateGameResult>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var newGame = new Model.Game
        {
            Id = Guid.NewGuid(),
            OwnerId = request.UserId,
            MinRate = request.RateRange.Min,
            MaxRate = request.RateRange.Max,
            GameState = GameState.Created,
            GameMap = Enumerable.Range(0, 9).Select(_ => GameMapSymbol.Empty).ToArray()
        };
        
        domainContext.Games.Add(newGame);

        await domainContext.SaveChangesAsync(cancellationToken);

        return new CreateGameResult(newGame.Id);
    }
}