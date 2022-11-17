using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.SqlSugar;

namespace WP.User.Repository.Entities
{
    public class SysRole : AuditInfo
    {

        public string Name { get; set; }

        public int Sort { get; set; }

        public string Desc { get; set; }


        [Navigate(typeof(SysUserRole), nameof(SysUserRole.RoleId), nameof(SysUserRole.UserId))]//注意顺序
        public List<SysUser> Users { get; set; }//只能是null不能赋默认值

    }
}
