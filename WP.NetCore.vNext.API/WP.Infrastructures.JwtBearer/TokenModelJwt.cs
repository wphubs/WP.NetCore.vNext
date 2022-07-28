﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Infrastructures.JwtBearer
{
    public class TokenModelJwt
    {
        public long Id { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
    }
}
