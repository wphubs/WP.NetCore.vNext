using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.SqlSugar;

namespace WP.User.Repository.Entities
{

    public class SysMenu
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Component { get; set; }
        public string Redirect { get; set; }
        public string Title { get; set; }
        public bool Affix { get; set; }
        public string Icon { get; set; }


        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool? Hidden { get; set; }

        /// <summary>
        /// 是否是菜单1:菜单,0:按钮
        /// </summary>
        public bool IsMenu { get; set; }

        /// <summary>
        /// 状态1:启用,0:禁用
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 父菜单编号
        /// </summary>
        public string PCode { get; set; }


        public string Url { get; set; }
    }

}
