using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.User.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<SysUser> GetUserInfo(string account);
    }
}
