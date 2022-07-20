using MediatR;
using Microsoft.AspNetCore.Mvc;
using WP.Infrastructures.EventBus.InMemory;

namespace WP.Shared.WebApi.Controller
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private readonly DomainNotificationHandler notifications;
        public ApiController(INotificationHandler<DomainNotification> notifications)
        {
            this.notifications = (DomainNotificationHandler)notifications;
        }

        protected ActionResult CustomResponse(object result = null)
        {
            var domainNotifications = notifications.GetNotifications();
            if (domainNotifications.Any())
            {
                return BadRequest(domainNotifications);
            }
            return Ok(result);
       
        }


    }
}
