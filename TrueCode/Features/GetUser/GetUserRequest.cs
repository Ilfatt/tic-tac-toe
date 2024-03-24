using Infrastructure;

namespace Features.GetUser;

public record GetUserRequest(Guid UserId, string Domain) : IHttpRequest<GetUserResponse>;