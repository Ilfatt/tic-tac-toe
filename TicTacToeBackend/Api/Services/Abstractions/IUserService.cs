namespace Api.Services.Abstractions;

public interface IUserService
{
    public Guid? GetUserIdOrDefault();
}