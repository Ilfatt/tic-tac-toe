using Api.Services.Abstractions;
using Core.Contracts;
using Core.Features.Game.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.Endpoints.Game.Create;

public class CreateGameCommandHandler(
    [FromServices] IUserService userService,
    IDbContext domainContext)
    : ICommandHandler<CreateGameCommand, CreateGameResult>
{ 
    public async Task<Result<CreateGameResult>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var userId = userService.GetUserIdOrDefault();
        if (userId is null)
            return 400;

        var newGame = new Model.Game
        {
            Id = Guid.NewGuid(),
            OwnerId = userId.Value,
            MinRate = request.RateRange.Min,
            MaxRate = request.RateRange.Max,
            GameState = GameState.Created
        };
        
        domainContext.Games.Add(newGame);

        await domainContext.SaveChangesAsync(cancellationToken);

        return new CreateGameResult(newGame.Id);
    }
}