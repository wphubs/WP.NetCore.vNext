using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Infrastructures.SqlSugar
{
    public class Entity : IEntity<long>
    {
        //[SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        [Newtonsoft.Json.JsonConverter(typeof(ValueToStringConverter))]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
    }
}
