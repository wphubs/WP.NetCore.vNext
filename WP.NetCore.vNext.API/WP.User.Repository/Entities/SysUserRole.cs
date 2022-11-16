using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.SqlSugar;

namespace WP.User.Repository.Entities
{
    public class SysUserRole : AuditInfo
    {
        /// <summary>
        /// 用户id
        /// </summary>

        public long UserId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>

        public long RoleId { get; set; }
    }
}
