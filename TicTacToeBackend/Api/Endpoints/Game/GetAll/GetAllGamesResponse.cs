using Core.Features.Game.GetAll;

namespace Api.Endpoints.Game.GetAll;

public record GetAllGamesResponse(IEnumerable<GetGameResult> GetGamesResult);