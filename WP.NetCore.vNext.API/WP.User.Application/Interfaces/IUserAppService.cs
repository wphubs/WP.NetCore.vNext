namespace WP.User.Application.Interfaces;

public interface IUserAppService
{
    Task<AppResult> CreateUserAsync(UserCreateOrUpdate userCreateOrUpdate);

}
