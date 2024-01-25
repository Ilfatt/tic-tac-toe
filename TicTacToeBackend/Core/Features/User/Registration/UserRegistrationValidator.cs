using FluentValidation;

namespace Core.Features.User.Registration;

public class UserRegistrationValidator : AbstractValidator<UserRegistrationCommand>
{
	public UserRegistrationValidator()
	{
		RuleFor(x => x.Username)
			.MaximumLength(30)
			.MinimumLength(5);
		RuleFor(x => x.Password)
			.MaximumLength(30)
			.MinimumLength(5);
	}
}