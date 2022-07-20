namespace WP.User.Application.Interfaces;

public interface IAccountAppService
{
    Task<bool> UserAccountAsync(UserLoginDto userLoginDto);

    Task<UserInfoDto> GetUserInfo(string account);

}
