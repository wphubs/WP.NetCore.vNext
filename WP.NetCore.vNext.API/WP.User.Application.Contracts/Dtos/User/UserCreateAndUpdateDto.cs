using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.User.Application.Contracts.Dtos.User
{
    public class UserCreateAndUpdateDto
    {
        public string Account { get; set; }

        public long? DeptId { get; set; }

        public string Name { get; set; }

        public int? Sex { get; set; }

        public string Password { get; set; }

    }
}
