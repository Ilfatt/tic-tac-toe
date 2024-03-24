using System.Net;

namespace Asp.Net;

public abstract class BaseEndpoint
{
	public abstract string Path { get; }
	
	public abstract HttpMethod HttpMethod { get; }

	public abstract Task ExecuteAsync(HttpListenerContext httpListenerContext);
}