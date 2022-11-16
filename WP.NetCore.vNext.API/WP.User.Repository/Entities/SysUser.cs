using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WP.Infrastructures.SqlSugar;

namespace WP.User.Repository.Entities
{
    public class SysUser : AuditInfo
    {

        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Avatar { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 账户状态
        /// </summary>
        public int Status { get; set; } = 1;


        [Navigate(typeof(SysUserRole), nameof(SysUserRole.UserId), nameof(SysUserRole.RoleId))]
        public List<SysRole> Roles { get; set; }

    }
}
