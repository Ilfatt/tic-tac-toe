namespace MediatR;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}