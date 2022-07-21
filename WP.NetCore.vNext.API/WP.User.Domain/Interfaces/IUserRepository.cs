using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.User.Domain.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<SysUser> GetUserInfo(string account);
    }
}
