using MediatR;

namespace Infrastructure;

public interface IHttpRequestHandler<in TRequest, TResponse> : IRequestHandler<TRequest, HttpResult<TResponse>>
	where TRequest : IHttpRequest<TResponse>
{
}