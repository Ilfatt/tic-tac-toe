using System.Net;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Asp.Net;

public class WebAppBuilder
{
	private readonly HttpListener _httpListener = new();

	private readonly List<EndpointShortInfo> _endpointsShortInfos = new();

	private static readonly Type BaseEndpointType = typeof(BaseEndpoint);

	private WebAppBuilder()
	{
	}

	public ServiceCollection ServiceCollection { get; } = new();

	public Configuration Configuration { get; } = new();

	public WebApp Build()
	{
		return new WebApp(_httpListener, ServiceCollection.BuildServiceProvider(), _endpointsShortInfos);
	}

	public void AddEndpointsFromAssembly<TTypeFromAssembly>()
	{
		var assembly = typeof(TTypeFromAssembly).Assembly;
		var endpointsShortInfo = assembly
			.GetTypes()
			.Where(x => x.IsAssignableTo(BaseEndpointType))
			.Where(x => x is { IsClass: true, IsInterface: false, IsAbstract: false })
			.Select(x => new EndpointShortInfo(
				path: (string)x
					.GetField("Path", BindingFlags.Public | BindingFlags.GetProperty)!
					.GetValue(null)!,
				httpMethod: (HttpMethod)x
					.GetField("HttpMethod", BindingFlags.Public | BindingFlags.GetProperty)!
					.GetValue(null)!,
				endpointType: x
			))
			.ToList();

		_endpointsShortInfos.AddRange(endpointsShortInfo);

		var existRouteDuplicate = endpointsShortInfo.Count != _endpointsShortInfos
			.Select(x => x.HttpMethod + x.Path)
			.Distinct()
			.Count();

		if (existRouteDuplicate)
			throw new ApplicationException("Конфликт роутов: есть несколько ручек с одним методом и маршрутом");

		foreach (var endpointType in endpointsShortInfo.Select(x => x.EndpointType))
		{
			ServiceCollection.AddTransient(BaseEndpointType, endpointType);
		}
	}

	public static WebAppBuilder Create()
	{
		var builder = new WebAppBuilder();

		builder.LoadConfiguration();
		builder._httpListener.Prefixes.Add(builder.Configuration["profiles:http:applicationUrl"]);
		builder.ServiceCollection.AddSingleton<IConfiguration>(builder.Configuration);

		return builder;
	}

	//TODO: метод заглушка, реализовать загрузку конфигурации из файла
	private void LoadConfiguration()
	{
		Configuration.Add(
			"ConnectionStrings:PostreSQL",
			"Server=localhost;Port=3003;Database=RugramAuth;User Id=dev;Password=111;Include Error Detail=true;");
		Configuration.Add(
			"profiles:http:applicationUrl",
			"http://localhost:5000/");
	}
}