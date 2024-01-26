using MediatR;

namespace Core.Features.Game.Create;

public record CreateGameCommand(RateRange RateRange, Guid UserId) : ICommand<CreateGameResult>;