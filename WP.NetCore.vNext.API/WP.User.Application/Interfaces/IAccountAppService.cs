namespace WP.User.Application.Interfaces;

public interface IAccountAppService
{
    Task UserAccountAsync(UserLoginDto userLoginDto);

}
