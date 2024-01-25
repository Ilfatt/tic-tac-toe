namespace Core.Features.User.Login;

public record UserLoginResult(string JwtToken, string Username, int UserRating);