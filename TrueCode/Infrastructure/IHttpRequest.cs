using MediatR;

namespace Infrastructure;

public interface IHttpRequest<TResponse> : IRequest<HttpResult<TResponse>>
{
}