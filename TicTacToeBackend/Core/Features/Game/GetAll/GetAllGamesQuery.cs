using MediatR;

namespace Core.Features.Game.GetAll;

public record GetAllGamesQuery(int Size, int Page) : IQuery<GetAllGamesResult>;
