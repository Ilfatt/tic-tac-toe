using MediatR;

namespace Core.Features.Game.Join;

public record JoinGameCommand(Guid GameId, Guid UserId) : ICommand<JoinGameResult>;
