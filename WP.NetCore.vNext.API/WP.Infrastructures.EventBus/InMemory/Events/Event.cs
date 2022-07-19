using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Infrastructures.EventBus.InMemory
{
    public abstract class Event : Message, INotification
    {
        // 时间戳
        public DateTime Timestamp { get; private set; }

        // 每一个事件都是有状态的
        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
