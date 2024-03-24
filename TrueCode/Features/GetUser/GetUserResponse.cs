using Features.Shared.Dto;

namespace Features.GetUser;

public record GetUserResponse(Guid UserId, string Name, string Domain, IEnumerable<TagDto>? Tags);