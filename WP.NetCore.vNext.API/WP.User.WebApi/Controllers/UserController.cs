using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WP.Shared.WebApi.Controller;
using WP.User.Application.Dtos;
using WP.User.Application.Interfaces;
using WP.User.Application.Services;

namespace WP.User.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
        private readonly IUserAppService userAppService;

        public UserController(IUserAppService userAppService)
        {
            this.userAppService = userAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserCreateOrUpdate userCreateOrUpdate)
        {
            return Result(await userAppService.CreateUserAsync(userCreateOrUpdate));
        }
    }
}
