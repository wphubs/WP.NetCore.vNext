using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.SqlSugar;
using WP.Shared.Application.Contracts;
using WP.User.Application.Contracts.Dtos.User;
using WP.User.Repository.Entities;

namespace WP.User.Application.Contracts.Services
{
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
        Task<ResponseResult<long>> CreateUserAsync(UserCreateAndUpdateDto input);

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
        Task<SqlSugarPagedList<SysUser>> GetUserListAsync(UserSearchPagedDto input);
    }
}
