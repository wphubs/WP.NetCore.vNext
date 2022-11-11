using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.User.Application.Contracts.Dtos.User
{
    public class UserInfoDto
    {
        public long Id { get; set; }

        public string Account { get; set; }

        public long? DeptId { get; set; }

        public string Name { get; set; }

        public int? Sex { get; set; }


        public List<UserInfoRoles> Roles { get; set; } = new ();

        public List<string> Permissions { get; private set; } = new ();

    }

    public class UserInfoRoles
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
