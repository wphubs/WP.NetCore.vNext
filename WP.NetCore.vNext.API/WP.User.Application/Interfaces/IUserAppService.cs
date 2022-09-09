using WP.Infrastructures.SqlSugar;

namespace WP.User.Application.Interfaces;

public interface IUserAppService
{


    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    Task<SqlSugarPagedList<UserInfoVM>> GetUserListAsync(int pageIndex, int pageSize);

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    Task<UserInfoVM> GetUserInfoAsync(string account);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveUserAsync(long id);


    /// <summary>
    /// 修改用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task UpdateUserAsync(UserCreateOrUpdateDto userCreateOrUpdate);




    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="userCreateOrUpdate"></param>
    /// <returns></returns>
    Task CreateUserAsync(UserCreateOrUpdateDto userCreateOrUpdate);

}
