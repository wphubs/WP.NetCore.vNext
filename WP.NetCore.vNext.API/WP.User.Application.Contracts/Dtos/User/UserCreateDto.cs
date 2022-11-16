using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.User.Application.Contracts.Dtos.User
{
    public class UserCreateDto: UserCreateAndUpdateDto
    {
        public string Password { get; set; }

    }
}
