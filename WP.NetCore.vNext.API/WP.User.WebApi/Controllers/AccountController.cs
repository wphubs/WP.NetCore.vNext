using WP.Infrastructures.JwtBearer;
using WP.Shared.WebApi.Controller;
using WP.User.Application.Contracts.Dtos.User;
using WP.User.Application.Contracts.Services;
using WP.Shared.Application.Contracts;
namespace WP.User.WebApi.Controllers;

[Route("api/Account")]
[ApiController]
public class AccountController : ApiController
{
    private readonly IAccountAppService accountAppService;

    public AccountController(IAccountAppService accountAppService)
    {
        this.accountAppService = accountAppService;
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="loginUser"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<UserTokenInfoDto>> Post(UserLoginDto loginUser)
    {
        loginUser.Account = "admin";
        loginUser.Password = "670b14728ad9902aecba32e22fa4f6bd";
        return Result(await accountAppService.UserAccountAsync(loginUser));

    }

}
