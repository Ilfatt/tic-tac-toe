using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions;

public static class AuthorizationConfiguration
{
	/// <summary>
	///     Добавление авторизации
	/// </summary>
	/// <param name="builder">WebApplicationBuilder</param>
	public static void AddAuthorization(this WebApplicationBuilder builder)
	{
		builder.Services.AddAuthorization();
		builder.Services.AddAuthentication(config =>
			{
				config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				config.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
				config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				if (builder.Environment.IsDevelopment()) options.RequireHttpsMetadata = false;

				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = builder.Configuration["AuthOptions:Issuer"],
					ValidateAudience = true,
					ValidAudience = builder.Configuration["AuthOptions:Audience"],
					ValidateLifetime = true,
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.ASCII.GetBytes(builder.Configuration["AuthOptions:JwtSecretKey"]!)),
					ValidateIssuerSigningKey = true
				};
			});
	}
}