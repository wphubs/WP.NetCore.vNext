﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Infrastructures.EventBus.InMemory
{
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
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

    
        // 事件存储Id
        public Guid Id { get; private set; }
        // 存储的数据
        public string Data { get; private set; }
        // 用户信息
        public string User { get; private set; }
    }
}
