using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using SqlSugar;
using WP.Infrastructures.Core.Helper;
namespace WP.Infrastructures.SchedulerJob
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSchedulerJob(this IServiceCollection services)
        {
            services.AddSingleton<HttpHelper>();
            services.AddSingleton<IJobFactory, SchedulerJobFactory>();
            services.AddTransient<HttpJob>();
            services.AddSingleton<ISchedulerCenter, SchedulerCenter>();
            return services;
        }

        public static void UseScheduleJob(this IApplicationBuilder app, ISqlSugarClient dbContext, ISchedulerCenter schedulerCenter)
        {
            var listJob = dbContext.Queryable<ScheduleJob>().ToList();
            listJob.ForEach(item =>
            {
                if (item.IsStart)
                {
                    try
                    {
                        schedulerCenter.AddScheduleJobAsync(item);
                        Console.WriteLine($"ScheduleJob[{item.JobName}] Start");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ScheduleJob[{item.JobName}] Fail ,Error:{ex.ToString()}");
                    }
                }
            });
        }
    }
}
