namespace Api.Events;

public record GameStartEvent(Guid GameId, Guid Owner, Guid Opponent);

public record PlayerMoveEvent(Guid GameId, string[] Board, Guid CurrentTurnId);

public record GameOverEvent(Guid GameId, string[] Board, Guid? WinnerId);

public record GameRestartEvent(Guid OldGameId, Guid GameId, Guid Host, Guid Opponent);