
namespace WP.User.Application.Contracts.Services;

public interface IRoleAppService
{
    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ResponseResult> DeleteRoleAsync(long id);

    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ResponseResult<long>> CreateRoleAsync(RoleCreateDto input);



    /// <summary>
    /// 修改角色
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ResponseResult<long>> UpdateRoleAsync(long id,RoleUpdateDto input);




    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<SqlSugarPagedList<RoleDto>> GetRoleListAsync(RoleSearchPagedDto input);
}
