using MediatR;

namespace Core.Features.User.Registration;

public record UserRegistrationCommand(string Username, string Password) : ICommand<UserRegistrationResult>;