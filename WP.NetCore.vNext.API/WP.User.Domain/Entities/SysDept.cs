using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.SqlSugar;

namespace WP.User.Domain.Entities
{
    public class SysDept : AuditInfo
    {
        [SugarColumn(IsNullable = true)]
        public string Name { get; set; }

        public int Ordinal { get; set; }

        public long? Pid { get; set; }

        [SugarColumn(IsNullable = true)]
        public string Pids { get; set; }

        [SugarColumn(IsNullable = true)]
        public string Desc { get; set; }


    }
}
