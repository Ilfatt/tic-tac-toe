namespace Asp.Net;

public class EndpointShortInfo(string path, HttpMethod httpMethod, Type endpointType)
{
	public string Path { get; } = path;
	
	public HttpMethod HttpMethod { get;} = httpMethod;
	
	public Type EndpointType { get; } = endpointType;
}