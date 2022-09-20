using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.JwtBearer;
using WP.Shared.Application.Contracts;

namespace WP.User.Application.Services
{
    [UseDependencyInjection]
    public class AccountAppService:AbstractAppService, IAccountAppService
    {
        private readonly ISqlSugarRepository<SysUser> userRepository;

        public AccountAppService(ISqlSugarRepository<SysUser> userRepository)
        {
            this.userRepository = userRepository;
        }


        public async Task<ResponseResult<UserTokenInfoDto>> UserAccountAsync(UserLoginDto userLoginDto)
        {
            var userInfo = await userRepository.FirstOrDefaultAsync(x => x.Account == userLoginDto.Account);
            if (userInfo == null)
            {
                return Problem(HttpStatusCode.Forbidden, "用户名或密码错误"); 
            }
            if (InfraHelper.Hash.GetHashedString(HashType.MD5, userLoginDto.Password, userInfo.Salt) != userInfo.Password)
            {
                return Problem(HttpStatusCode.Forbidden, "用户名或密码错误"); 
            }
            var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
                    {
                        { "Id", userInfo.Id },
                        { "Account", userInfo.Account  },
                        { "Name", userInfo.Name  },
                    });
            var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken);
            return new UserTokenInfoDto() { Token = accessToken, RefreshToken = refreshToken };
        }
    }
}
