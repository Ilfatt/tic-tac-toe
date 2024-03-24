using Features.Shared.Dto;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Features.GetUser;

public class GetUserRequestHandler(IAppDbContext appDbContext) : IHttpRequestHandler<GetUserRequest, GetUserResponse>
{
	public async Task<HttpResult<GetUserResponse>> Handle(GetUserRequest request, CancellationToken cancellationToken)
	{
		var response = await appDbContext.Users
			.Where(x => x.UserId == request.UserId && x.Domain == request.Domain)
			.Select(x => new GetUserResponse(
				request.UserId,
				x.Name,
				request.Domain,
				x.TagToUsers!
					.Select(tagToUser => new TagDto(
						tagToUser.TagId,
						tagToUser.Tag!.Value,
						tagToUser.Tag!.Domain))))
			.FirstOrDefaultAsync(cancellationToken);

		if (response is null)
			return 404;

		return response;
	}
}