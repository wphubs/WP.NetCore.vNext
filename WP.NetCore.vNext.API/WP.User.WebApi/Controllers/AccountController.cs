using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WP.Infrastructures.EventBus.InMemory;
using WP.Shared.WebApi.Controller;
using WP.User.Application.Dtos;
using WP.User.Application.Interfaces;

namespace WP.User.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly IAccountAppService accountAppService;
        private readonly DomainNotificationHandler notifications;

        public AccountController(IAccountAppService accountAppService, INotificationHandler<DomainNotification> notifications)
        {
            this.accountAppService = accountAppService;
            this.notifications = (DomainNotificationHandler)notifications;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserLoginDto loginUser)
        {
            await accountAppService.UserAccountAsync(loginUser);
            var bb = notifications.GetNotifications();
            return Ok(bb);
        }

    }
}
