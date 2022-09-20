using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Shared.Application;
using WP.Shared.Application.Contracts;
using WP.User.Application.Contracts.Dtos.User;

namespace WP.User.Application.Contracts.Services
{
    public interface IAccountAppService
    {
        Task<ResponseResult<UserTokenInfoDto>> UserAccountAsync(UserLoginDto userLoginDto);
    }
}
