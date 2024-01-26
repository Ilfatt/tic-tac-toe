namespace Core.Features.Game.Join;

public record JoinGameResult(Guid GameId, Guid? OwnerId, Guid? OpponentId, GameProgress GameProgress);