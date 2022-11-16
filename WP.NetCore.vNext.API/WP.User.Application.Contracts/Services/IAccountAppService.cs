

namespace WP.User.Application.Contracts.Services
{
    public interface IAccountAppService
    {
        Task<ResponseResult<UserTokenInfoDto>> UserAccountAsync(UserLoginDto userLoginDto);

    }
}
