using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.User.Domain.Interfaces
{
    public interface IUserRepository
    {

        Task<List<SysUser>> GetUserListAsync();

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<SysUser> GetUserInfoAsync(string account);


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveUserAsync(long id);
    }
}
