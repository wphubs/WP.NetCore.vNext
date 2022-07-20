using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Infrastructures.Core.Context
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public long Role { get; set; }


        public List<string> Urls { get; set; }


    }
}
