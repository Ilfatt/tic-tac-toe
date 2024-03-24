using Features.Shared.Dto;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Features.GetAllUsersByDomain;

public class GetAllUsersByDomainRequestHandler(IAppDbContext appDbContext)
	: IHttpRequestHandler<GetAllUsersByDomainRequest, GetAllUsersByDomainResponse>
{
	public async Task<HttpResult<GetAllUsersByDomainResponse>> Handle(
		GetAllUsersByDomainRequest request,
		CancellationToken cancellationToken)
	{
		var users = await appDbContext.Users
			.Where(x => x.Domain == request.Domain)
			.OrderBy(x => x.UserId)
			.Skip(request.PageNumber * request.PageSize)
			.Take(request.PageSize)
			.Select(x => new UserWithTagsDto(
				x.UserId,
				x.Name,
				request.Domain,
				x.TagToUsers!
					.Select(tagToUser => new TagDto(
						tagToUser.TagId,
						tagToUser.Tag!.Value,
						tagToUser.Tag!.Domain))))
			.ToListAsync(cancellationToken);

		if (users.Count == 0)
			return 404;

		return new GetAllUsersByDomainResponse(users);
	}
}