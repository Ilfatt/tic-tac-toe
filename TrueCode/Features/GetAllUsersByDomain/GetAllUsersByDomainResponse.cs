using Features.Shared.Dto;

namespace Features.GetAllUsersByDomain;

public record GetAllUsersByDomainResponse(IEnumerable<UserWithTagsDto> Users);

public record UserWithTagsDto(Guid UserId, string Name, string Domain, IEnumerable<TagDto>? Tags);