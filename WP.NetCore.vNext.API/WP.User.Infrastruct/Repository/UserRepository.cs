using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.Core;
using WP.Infrastructures.SqlSugar;
using WP.User.Domain.Entities;
using WP.User.Domain.Interfaces;

namespace WP.User.Infrastruct
{
    [UseDependencyInjection]
    public class UserRepository: IUserRepository
    {
        private readonly ISqlSugarRepository<SysUser> userRepository;

        public UserRepository(ISqlSugarRepository<SysUser> userRepository)
        {
            this.userRepository = userRepository;
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<SysUser> GetUserInfo(string account)
        {
            return await userRepository.FirstOrDefaultAsync(x => x.Account == account);
        }
    }
}
