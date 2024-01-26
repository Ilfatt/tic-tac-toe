namespace Core.Features.Game.GetAll;

public record GetAllGamesResult(IEnumerable<GetGameResult> GetGamesResult, int TotalCount);