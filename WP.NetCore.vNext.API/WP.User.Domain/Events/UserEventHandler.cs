using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.User.Domain.Events
{
    public class UserEventHandler : INotificationHandler<CreatedStudentEvent>
    {

        /// <summary>
        /// 创建用户成功
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(CreatedStudentEvent notification, CancellationToken cancellationToken)
        {
            //todo:邮件通知、系统通知
            return Task.CompletedTask;
        }
    }
}
