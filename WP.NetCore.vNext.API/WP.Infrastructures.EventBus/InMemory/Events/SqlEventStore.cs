using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.Core.Context;

namespace WP.Infrastructures.EventBus.InMemory.Events
{
    public class SqlEventStore : IEventStoreService
    {
        private readonly IUserContext userContext;
        private readonly ISqlSugarClient sqlSugarClient;

        public SqlEventStore(IUserContext userContext, ISqlSugarClient sqlSugarClient)
        {
            this.userContext = userContext;
            this.sqlSugarClient = sqlSugarClient;
        }

        /// <summary>
        /// 保存事件模型统一方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theEvent"></param>
        public void Save<T>(T theEvent) where T : Event
        {
            // 对事件模型序列化
            var serializedData = JsonConvert.SerializeObject(theEvent);
            userContext.Name = "123";
            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                userContext.Name);

            sqlSugarClient.Insertable(storedEvent).ExecuteCommand();
            //_eventStoreRepository.Store(storedEvent);

        }
    }
}
