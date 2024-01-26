using Core.Features.Game;

namespace Api.Endpoints.Game.Join;

public record JoinGameResponse(
    Guid GameId = default,
    Guid? OwnerId = default,
    Guid? OpponentId = default,
    RateRange RateRange = default,
    GameProgress GameProgress = default);
