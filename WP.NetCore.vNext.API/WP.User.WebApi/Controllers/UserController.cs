

namespace WP.User.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ApiController
{
    private readonly IUserAppService userAppService;

    public UserController(IUserAppService userAppService)
    {
        this.userAppService = userAppService;
    }


    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] long id) => Result(await userAppService.DeleteUserAsync(id));

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<long>> Post([FromBody] UserCreateDto input) => CreatedResult(await userAppService.CreateUserAsync(input));

    /// <summary>
    /// 修改用户信息
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromRoute] long id, [FromBody] UserUpdateDto input) => Result(await userAppService.UpdateUserAsync(id, input));

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetUserInfo")]
    public async Task<ActionResult<UserInfoDto>> GetUserInfo() => await userAppService.GetUserInfoAsync(353939986944069);

    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<SqlSugarPagedList<UserDto>>> Get([FromQuery] UserSearchPagedDto input) => await userAppService.GetUserListAsync(input);





}
