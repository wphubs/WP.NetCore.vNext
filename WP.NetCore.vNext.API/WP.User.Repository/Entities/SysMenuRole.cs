using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.SqlSugar;

namespace WP.User.Repository.Entities
{
    public class SysMenuRole : AuditInfo
    {
        public long MenuId { get; set; }

        public long RoleId { get; set; }
    }
}
