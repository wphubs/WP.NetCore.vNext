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
        public long? DeptId { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }

        public long? PId { get; set; }

        public string Desc { get; set; }

    }
}
