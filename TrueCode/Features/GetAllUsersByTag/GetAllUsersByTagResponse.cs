namespace Features.GetAllUsersByTag;

public record GetAllUsersByTagResponse(IEnumerable<UserDto> Users);

public record UserDto(Guid UserId, string Name, string Domain);