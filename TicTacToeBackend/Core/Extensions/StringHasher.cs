using System.Security.Cryptography;
using System.Text;

namespace Core.Extensions;

public static class StringHasher
{
	public static string HashSha256(this string inputString)
	{
		var inputBytes = Encoding.UTF32.GetBytes(inputString);
		var hashedBytes = SHA256.HashData(inputBytes);

		return Convert.ToBase64String(hashedBytes);
	}
}