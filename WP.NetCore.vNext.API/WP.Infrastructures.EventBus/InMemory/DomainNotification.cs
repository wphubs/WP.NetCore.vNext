﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.EventBus.InMemory;

namespace WP.Infrastructures.EventBus.InMemory
{
    public class DomainNotification : Event
    {
        // 标识
        public Guid DomainNotificationId { get; private set; }
        // 键（可以根据这个key，获取当前key下的全部通知信息）
        public string Key { get; private set; }
        // 值（与key对应）
        public string Value { get; private set; }
        // 版本信息
        public int Version { get; private set; }

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}
