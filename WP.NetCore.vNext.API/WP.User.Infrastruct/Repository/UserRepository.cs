﻿using SqlSugar;
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
        private readonly ISqlSugarClient sqlSugarClient;
        private readonly ISqlSugarRepository<SysUser> userRepository;

        public UserRepository(ISqlSugarClient sqlSugarClient, ISqlSugarRepository<SysUser> userRepository)
        {
            this.sqlSugarClient = sqlSugarClient;
            this.userRepository = userRepository;
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveAsync(long id)
        {
             await sqlSugarClient.Updateable<SysUser>().SetColumns(x => x.IsDeleted == true).Where(x=>x.Id==id).ExecuteCommandAsync();
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<SysUser> GetAsync(string account)
        {
            return await userRepository.FirstOrDefaultAsync(x => x.Account == account);
        }
    }
}
