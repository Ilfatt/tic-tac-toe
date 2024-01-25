using MediatR;

namespace Core.Features.User.GetUserData;

public record GetUserDataQuery(Guid UserId) : IQuery<GetUserDataResult>;