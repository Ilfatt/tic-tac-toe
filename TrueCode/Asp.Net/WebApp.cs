using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace Asp.Net;

public class WebApp(
	HttpListener httpListener,
	IServiceProvider serviceProvider,
	IEnumerable<EndpointShortInfo> endpointShortInfos)
{
	public readonly IServiceProvider ServiceProvider = serviceProvider;

	public async Task RunAsync()
	{
		httpListener.Start();

		while (httpListener.IsListening)
		{
			var context = await httpListener.GetContextAsync();
			var response = context.Response;
			var request = context.Request;
			await using var scope = ServiceProvider.CreateAsyncScope();
			var endpointType = endpointShortInfos
				.Where(x =>
					x.Path == request.Url?.LocalPath
					&& x.HttpMethod.ToString() == request.HttpMethod)
				.Select(x => x.EndpointType)
				.FirstOrDefault();

			if (endpointType is null)
			{
				response.StatusCode = 404;
				response.OutputStream.Close();
				response.Close();
				continue;
			}

			var endpoint = (BaseEndpoint)(scope.ServiceProvider.GetService(endpointType)
			                          ?? throw new ApplicationException("Нужная ручка не добавлена в DI"));

			await endpoint.ExecuteAsync(context);

			response.OutputStream.Close();
			response.Close();
		}

		httpListener.Stop();
		httpListener.Close();
	}
}