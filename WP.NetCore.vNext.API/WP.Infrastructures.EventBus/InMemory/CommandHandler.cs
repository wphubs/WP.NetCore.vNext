using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.EventBus.InMemory;
using FluentValidation;
using FluentValidation.Results;
using WP.Infrastructures.Core;

namespace WP.Infrastructures.EventBus.InMemory
{
    public class CommandHandler
    {
        // 注入工作单元
        //private readonly IUnitOfWork _uow;
        // 注入中介处理接口（目前用不到，在领域事件中用来发布事件）
        private readonly IMediatorHandler _bus;
        protected ValidationResult ValidationResult;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="bus"></param>
        /// <param name="cache"></param>
        public CommandHandler( IMediatorHandler bus)
        {
            //IUnitOfWork uow, _uow = uow;
            _bus = bus;
            ValidationResult = new ValidationResult();

        }

        /// <summary>
        /// 发布领域验证错误事件
        /// </summary>
        /// <param name="message"></param>
        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                //将错误信息提交到事件总线，派发出去
                _bus.RaiseEvent(new DomainNotification("", error.ErrorMessage));
            }
        }

        /// <summary>
        /// 发布领域错误事件
        /// </summary>
        /// <param name="error"></param>
        protected void NotifyValidationErrors(string error)
        {
            _bus.RaiseEvent(new DomainNotification("", error));
        }


        /// <summary>
        /// 发布领域事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        protected void NotifyDomainEvent<T>(T @event) where T : Event
        {
            _bus.RaiseEvent(@event);
        }

        //工作单元提交
        //如果有错误，下一步会在这里添加领域通知
        public bool Commit()
        {
            //if (_uow.Commit())
                
                return true;

            return false;
        }
    }
}
