using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.Core;
using WP.Infrastructures.EventBus.InMemory;

namespace WP.Infrastructures.EventBus.InMemory
{
    public interface IMediatorHandler
    {
        /// <summary>
        /// 发送命令模型发布到中介者
        /// </summary>
        /// <typeparam name="T"> 泛型 </typeparam>
        /// <param name="command"> 命令模型</param>
        /// <returns></returns>
        Task<AppResult> SendCommand<T>(T command) where T : Command;


        /// <summary>
        /// 通过总线发布事件
        /// </summary>
        /// <typeparam name="T"> 泛型 继承 Event：INotification</typeparam>
        /// <param name="event"> 事件模型，比如StudentRegisteredEvent，</param>
        /// <returns></returns>
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
