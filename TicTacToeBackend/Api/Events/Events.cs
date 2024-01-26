using Core.Features.Game;

namespace Api.Events;

public record GameStartEvent(Guid GameId, Guid Owner, Guid Opponent, Guid CurrentTurnId);

public record GameMoveEvent(Guid GameId, GameMapSymbol[] Board, Guid CurrentTurnId);

public record GameFinishEvent(Guid GameId, GameMapSymbol[] Board, Guid? WinnerId);

public record GameRestartEvent(Guid OldGameId, Guid GameId, Guid Host, Guid Opponent);