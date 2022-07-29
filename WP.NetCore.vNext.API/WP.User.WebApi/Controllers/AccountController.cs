using WP.Infrastructures.JwtBearer;

namespace WP.User.WebApi.Controllers;

[Route("api/Account")]
[ApiController]
public class AccountController : ApiController
{
    private readonly IAccountAppService accountAppService;
    private readonly IUserAppService userAppService;

    public AccountController(IAccountAppService accountAppService, IUserAppService userAppService, INotificationHandler<DomainNotification> notifications):base(notifications)
    {
        this.accountAppService = accountAppService;
        this.userAppService = userAppService;
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
            var userInfo = await userAppService.GetUserInfoAsync(loginUser.Account);
            var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
            {
                { "Id", userInfo.Id },  
                { "UserName", userInfo.Account  },
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
