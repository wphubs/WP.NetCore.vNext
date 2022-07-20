using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.User.Application.Dtos
{
    public class UserInfoDto
    {

        public long Id { get; set; }

        public string Account { get; set; }

        public string Avatar { get; set; }

        public long? DeptId { get; set; }

        public string Name { get; set; }

        public int? Sex { get; set; }

    }
}
