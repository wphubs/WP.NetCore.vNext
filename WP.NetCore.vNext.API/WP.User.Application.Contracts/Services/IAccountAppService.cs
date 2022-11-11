
using WP.Shared.Application.Contracts;
using WP.User.Application.Contracts.Dtos.User;

namespace WP.User.Application.Contracts.Services
{
    public interface IAccountAppService
    {
        Task<ResponseResult<UserTokenInfoDto>> UserAccountAsync(UserLoginDto userLoginDto);

    }
}
