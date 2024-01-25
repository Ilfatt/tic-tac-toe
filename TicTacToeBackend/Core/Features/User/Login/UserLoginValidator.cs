using FluentValidation;

namespace Core.Features.User.Login;

public class UserLoginValidator : AbstractValidator<UserLoginQuery>
{
	public UserLoginValidator()
	{
		RuleFor(x => x.Username)
			.MaximumLength(30)
			.MinimumLength(5);
		RuleFor(x => x.Password)
			.MaximumLength(30)
			.MinimumLength(5);
	}
}