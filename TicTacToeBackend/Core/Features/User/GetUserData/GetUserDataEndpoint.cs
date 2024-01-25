using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TemporaryStorage;
using TemporaryStorage.Models;

namespace Core.Features.User.GetUserData;

public class GetUserDataQueryHandler
	(UserManager<IdentityUser<Guid>> userManager, IMongoDbStorage<UserRating> mongoDbStorage)
	: IQueryHandler<GetUserDataQuery, GetUserDataResult>
{
	public async Task<Result<GetUserDataResult>> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
	{
		var username = await userManager.Users
			               .Where(x => x.Id == request.UserId)
			               .Select(x => x.UserName)
			               .FirstOrDefaultAsync(cancellationToken: cancellationToken)
		               ?? throw new ArgumentException($"User with id: {request.UserId} not found");

		var userRating = await mongoDbStorage.GetByIdAsync(request.UserId)
		                 ?? throw new ArgumentException(
			                 $"UserRaing, for user with id: {request.UserId} not found. ");

		return new GetUserDataResult(username, userRating.Rating);
	}
}