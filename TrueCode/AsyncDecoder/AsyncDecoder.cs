namespace AsyncDecoder;

public class AsyncDecoder(Stream stream, char delimiter, int maxMessageSize) : IAsyncEnumerable<string>
{
	private int _currentIndex;

	public async IAsyncEnumerator<string> GetAsyncEnumerator(
		CancellationToken cancellationToken = new CancellationToken())
	{
		var result = new char[maxMessageSize];
		var streamReader = new StreamReader(stream);

		while (!streamReader.EndOfStream && !cancellationToken.IsCancellationRequested)
		{
			await streamReader.ReadAsync(result, _currentIndex, 1);

			if (result[_currentIndex] != delimiter)
				_currentIndex++;

			result[_currentIndex] = default;
			_currentIndex = -1;

			var message = new string(result);

			for (var i = 0; i < result.Length; i++)
				result[0] = default;

			yield return message;
		}

		if (result[0] != default)
			yield return new string(result);
	}
}