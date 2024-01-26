using MediatR;

namespace Core.Features.User.Login;

public record UserLoginQuery(string Username, string Password) : IQuery<UserLoginResult>;