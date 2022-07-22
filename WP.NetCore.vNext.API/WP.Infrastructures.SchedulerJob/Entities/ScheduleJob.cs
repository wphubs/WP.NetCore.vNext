using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.SqlSugar;

namespace WP.Infrastructures.SchedulerJob
{
    public class ScheduleJob: AuditInfo
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 任务分组
        /// </summary>
        public string JobGroup { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public JobTypeEnum JobType { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// Cron表达式
        /// </summary>
        public string Cron { get; set; }
        /// <summary>
        /// Simple循环次数
        /// </summary>
        public int? SimpleTimes { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int ExecTimes { get; set; } = 0;

        /// <summary>
        /// 执行间隔时间，单位秒（如果有Cron，则IntervalSecond失效）
        /// </summary>
        public int? IntervalSecond { get; set; }
        /// <summary>
        /// 触发器类型
        /// </summary>
        public TriggerTypeEnum TriggerType { get; set; } = TriggerTypeEnum.Cron;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public bool IsStart { get; set; } = false;

        #region Url
        /// <summary>
        /// 请求url
        /// </summary>
        public string RequestUrl { get; set; }
        /// <summary>
        /// 请求参数（Post，Put请求用）
        /// </summary>
        public string RequestParameters { get; set; }
        /// <summary>
        /// Headers(可以包含如：Authorization授权认证)
        /// 格式：{"Authorization":"userpassword.."}
        /// </summary>
        public string Headers { get; set; }
        /// <summary>
        /// 请求类型
        /// </summary>
        public RequestTypeEnum RequestType { get; set; } = RequestTypeEnum.Post;

        #endregion
    }

    public enum JobTypeEnum
    {
        Http = 1,
        Assembly = 2,
        RabbitMQ = 3,
    }

    public enum TriggerTypeEnum
    {
        Cron = 1,
        Simple = 2,
    }
    public enum RequestTypeEnum
    {
        [Description("Get")]
        Get = 1,
        [Description("Post")]
        Post = 2,
        [Description("Put")]
        Put = 3,
        [Description("Delete")]
        Delete = 4
    }
}
