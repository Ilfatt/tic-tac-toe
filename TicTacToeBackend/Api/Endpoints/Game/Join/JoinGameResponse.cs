using Core.Features.Game;

namespace Api.Endpoints.Game.Join;

public record JoinGameResponse(
    Guid GameId,
    Guid? OwnerId,
    Guid? OpponentId,
    RateRange RateRange,
    GameProgress GameProgress);
