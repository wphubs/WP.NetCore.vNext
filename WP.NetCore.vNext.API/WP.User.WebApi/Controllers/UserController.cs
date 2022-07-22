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
