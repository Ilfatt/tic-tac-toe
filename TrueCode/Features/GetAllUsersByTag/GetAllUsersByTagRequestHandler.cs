using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Features.GetAllUsersByTag;

public class GetAllUsersByTagRequestHandler(IAppDbContext appDbContext)
	: IHttpRequestHandler<GetAllUsersByTagRequest, GetAllUsersByTagResponse>
{
	public async Task<HttpResult<GetAllUsersByTagResponse>> Handle(
		GetAllUsersByTagRequest request,
		CancellationToken cancellationToken)
	{
		var users = await appDbContext.Tags
			.Where(x => x.Value == request.TagValue && x.Domain == request.TagDomain)
			.SelectMany(x => x.TagToUsers!
				.Select(tagToUser => new UserDto(
					tagToUser.User!.UserId,
					tagToUser.User!.Name,
					tagToUser.User!.Domain)))
			.ToListAsync(cancellationToken);

		if (users.Count == 0)
			return 404;

		return new GetAllUsersByTagResponse(users);
	}
}