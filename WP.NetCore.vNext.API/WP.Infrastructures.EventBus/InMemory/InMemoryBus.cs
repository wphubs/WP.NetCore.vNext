﻿using MediatR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.Core;
using WP.Infrastructures.EventBus.InMemory;

namespace WP.Infrastructures.EventBus.InMemory
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        //构造函数注入
        private readonly IMediator _mediator;
        //注入服务工厂
        private readonly ServiceFactory _serviceFactory;
        private static readonly ConcurrentDictionary<Type, object> _requestHandlers = new ConcurrentDictionary<Type, object>();
        // 事件仓储服务
        private readonly IEventStoreService _eventStoreService;

        //
        public InMemoryBus(IMediator mediator, ServiceFactory serviceFactory, IEventStoreService eventStoreService)
        {
            _mediator = mediator;
            _serviceFactory = serviceFactory;
            _eventStoreService = eventStoreService;
        }

        /// <summary>
        /// 实现我们在IMediatorHandler中定义的接口
        /// 没有返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public Task<bool> SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        /// <summary>
        /// 引发事件的实现方法
        /// </summary>
        /// <typeparam name="T">泛型 继承 Event：INotification</typeparam>
        /// <param name="event">事件模型，比如StudentRegisteredEvent</param>
        /// <returns></returns>
        public Task RaiseEvent<T>(T @event) where T : Event
        {
            // 除了领域通知以外的事件都保存下来
            if (!@event.MessageType.Equals("DomainNotification"))
                _eventStoreService?.Save(@event);

            // MediatR中介者模式中的第二种方法，发布/订阅模式
            return _mediator.Publish(@event);
        }


    }
}
