using Microsoft.AspNetCore.Authorization;

namespace WP.User.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ApiController
{
    private readonly IUserAppService userAppService;

    public UserController(IUserAppService userAppService,INotificationHandler<DomainNotification> notifications):base(notifications)
    {
        this.userAppService = userAppService;
    }


    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var listUser=await userAppService.GetUserListAsync();
        return CustomResponse(listUser);
    }


    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="userCreateOrUpdate"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserCreateOrUpdateDto userCreateOrUpdate)
    {
        await userAppService.CreateUserAsync(userCreateOrUpdate);
        return CustomResponse();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetUserInfo")]
    public async Task<IActionResult> GetUserInfo()
    {
        var token = GetToken();
        var userInfo = await userAppService.GetUserInfoAsync(token.Account);
        return CustomResponse(userInfo);
    }



    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] long id)
    {
        await userAppService.RemoveUserAsync(id);
        return CustomResponse();
    }
}
