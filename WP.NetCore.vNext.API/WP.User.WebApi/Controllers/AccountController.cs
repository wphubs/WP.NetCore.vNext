using WP.Infrastructures.JwtBearer;

namespace WP.User.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ApiController
{
    private readonly IAccountAppService accountAppService;

    public AccountController(IAccountAppService accountAppService, INotificationHandler<DomainNotification> notifications):base(notifications)
    {
        this.accountAppService = accountAppService;
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="loginUser"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(UserLoginDto loginUser)
    {
        loginUser.Account = "admin";
        loginUser.Password = "670b14728ad9902aecba32e22fa4f6bd";
        var result=await accountAppService.UserAccountAsync(loginUser);
        if (result)
        {
            var userInfo = await accountAppService.GetUserInfo(loginUser.Account);
            var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
            {
                { "UserId", userInfo.Id },  
                { "Account", userInfo.Account  },
                { "Name", userInfo.Name  },
            });
            var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken);
            return CustomResponse(new UserTokenInfoDto() {Token= accessToken, RefreshToken = refreshToken });
        }
        else
        {
            return CustomResponse();
        }
       
    }

}
