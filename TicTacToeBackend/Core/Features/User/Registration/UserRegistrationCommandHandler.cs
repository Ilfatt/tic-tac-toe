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

namespace Core.Features.User.Registration;

public class UserRegistrationCommandHandler(
		UserManager<IdentityUser<Guid>> userManager,
		IConfiguration configuration,
		IMongoDbStorage<UserRating> mongoDbStorage)
	: ICommandHandler<UserRegistrationCommand, UserRegistrationResult>
{
	public async Task<Result<UserRegistrationResult>> Handle(
		UserRegistrationCommand request,
		CancellationToken cancellationToken)
	{
		var userWithThisNameExist = await userManager.Users
			.AnyAsync(user => user.UserName == request.Username, cancellationToken);

		if (userWithThisNameExist) return 409;

		var user = new IdentityUser<Guid>()
		{
			Id = Guid.NewGuid(),
			UserName = request.Username,
			PasswordHash = request.Password.HashSha256()
		};

		var userRating = new UserRating
		{
			Id = user.Id,
			Rating = 0
		};

		await userManager.CreateAsync(user);
		await mongoDbStorage.InsertAsync(userRating);

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

		return new UserRegistrationResult(tokenHandler.WriteToken(token));
	}
}