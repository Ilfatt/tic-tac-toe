namespace MediatR;

public class Result<TBody>
{
	private Result(int httpStatusCode)
	{
		HttpStatusCode = httpStatusCode;
	}

	private Result(TBody body)
	{
		Body = body;
		HttpStatusCode = 200;
	}

	public int HttpStatusCode { get; private init; }

	public TBody? Body { get; private init; }

	public static implicit operator Result<TBody>(TBody body)
	{
		return new Result<TBody>(body);
	}

	public static implicit operator Result<TBody>(int httpStatusCode)
	{
		return new Result<TBody>(httpStatusCode);
	}
}