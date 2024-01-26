namespace Api.Contracts;

public interface IEndpoint
{
	public void AddRoute(IEndpointRouteBuilder app);
}