using FluentValidation;

namespace MediatR;

public class ValidationBehavior<TRequest, TResponse>
	(IValidator<TRequest> validator)
	: IPipelineBehavior<TRequest, Result<TResponse>> where TRequest : IRequest<TResponse>
{
	public async Task<Result<TResponse>> Handle(
		TRequest request,
		RequestHandlerDelegate<Result<TResponse>> next,
		CancellationToken cancellationToken)
	{
		var validationResult = await validator.ValidateAsync(request, cancellationToken);

		if (!validationResult.IsValid) return 400;

		return await next();
	}
}