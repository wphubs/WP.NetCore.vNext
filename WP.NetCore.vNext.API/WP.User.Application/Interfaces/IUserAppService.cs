namespace WP.User.Application.Interfaces;

public interface IUserAppService
{


    Task<UserInfoDto> GetUserInfo(string account);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveUserAsync(long id);


    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="userCreateOrUpdate"></param>
    /// <returns></returns>
    Task CreateUserAsync(UserCreateOrUpdateDto userCreateOrUpdate);

}
