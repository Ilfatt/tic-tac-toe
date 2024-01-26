namespace MediatR;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;