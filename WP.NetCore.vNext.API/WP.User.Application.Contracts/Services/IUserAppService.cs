
namespace WP.User.Application.Contracts.Services;

public interface IUserAppService
{

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ResponseResult> DeleteUserAsync(long id);

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ResponseResult<long>> CreateUserAsync(UserCreateDto input);

    /// <summary>
    /// 修改用户信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ResponseResult> UpdateUserAsync(long id, UserUpdateDto input);

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<UserInfoDto> GetUserInfoAsync(long userId);

    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<SqlSugarPagedList<UserDto>> GetUserListAsync(UserSearchPagedDto input);
}
