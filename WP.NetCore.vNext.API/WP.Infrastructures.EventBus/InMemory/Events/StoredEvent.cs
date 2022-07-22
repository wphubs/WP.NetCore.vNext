using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Infrastructures.EventBus.InMemory
{

    [SugarTable("SysStored")]
    public class StoredEvent : Event
    {
        /// <summary>
        /// 构造方式实例化
        /// </summary>
        /// <param name="theEvent"></param>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public StoredEvent(Event theEvent, string data, string user)
        {
            Id = Core.IdGenerater.GetNextId();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        public StoredEvent()
        {

        }
    
        // 事件存储Id
        public long Id { get; private set; }
        // 存储的数据
        public string Data { get; private set; }
        // 用户信息
        public string User { get; private set; }
    }
}
