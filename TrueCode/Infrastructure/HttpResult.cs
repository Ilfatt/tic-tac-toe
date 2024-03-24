namespace Infrastructure;

public class HttpResult<TBody>
{
	private HttpResult(int httpStatusCode)
	{
		HttpStatusCode = httpStatusCode;
	}

	private HttpResult(TBody body)
	{
		Body = body;
		HttpStatusCode = 200;
	}

	public int HttpStatusCode { get; private init; }

	public TBody? Body { get; private init; }

	public static implicit operator HttpResult<TBody>(TBody body)
	{
		return new HttpResult<TBody>(body);
	}

	public static implicit operator HttpResult<TBody>(int httpStatusCode)
	{
		return new HttpResult<TBody>(httpStatusCode);
	}
}