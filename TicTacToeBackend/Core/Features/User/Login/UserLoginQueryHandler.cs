using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TemporaryStorage;
using TemporaryStorage.Models;

namespace Core.Features.User.Login;

public class UserLoginQueryHandler
	(UserManager<IdentityUser<Guid>> userManager,
		IConfiguration configuration, 
		IMongoDbStorage<UserRating> mongoDbStorage)
	: IQueryHandler<UserLoginQuery, UserLoginResult>
{
	public async Task<Result<UserLoginResult>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
	{
		var user = await userManager.Users.AsNoTracking()
			.FirstOrDefaultAsync(x => x.UserName == request.Username, cancellationToken);

		if (user is null) return 404;
		if (user.PasswordHash != request.Password.HashSha256()) return 403;

		var tokenHandler = new JwtSecurityTokenHandler();
		var jwtSecurityKey = Encoding.ASCII.GetBytes(configuration["AuthOptions:JwtSecretKey"]!);
		var claims = new List<Claim>
		{
			new(nameof(ClaimTypes.NameIdentifier), user.Id.ToString()),
		};

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Audience = configuration["AuthOptions:Audience"],
			Issuer = configuration["AuthOptions:Issuer"],
			Subject = new ClaimsIdentity(claims.ToArray()),
			Expires = DateTime.UtcNow.AddMinutes(int.Parse(
				configuration["AuthOptions:AccessTokenLifetimeInMinutes"]!)),
			SigningCredentials =
				new SigningCredentials(
					new SymmetricSecurityKey(jwtSecurityKey),
					SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		var userRating = await mongoDbStorage.GetByIdAsync(user.Id) 
		                 ?? throw new ArgumentException($"UserRaing, for user with id: {user.Id} not found. ");

		return new UserLoginResult(tokenHandler.WriteToken(token), user.UserName!, userRating.Rating);
	}
}