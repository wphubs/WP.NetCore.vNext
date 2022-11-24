using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WP.Infrastructures.Core;

namespace WP.Shared.WebApi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar
    {
        protected virtual void AddCorsPolicy()
        {
            Services.AddCors(options =>
            {
                // 配置策略
                options.AddPolicy(Appsettings.Get("CorsAccessorSettings", "PolicyName"), policy =>
                {
                    policy
                    .WithOrigins(Appsettings.Get("CorsAccessorSettings", "WithOrigins").Split(','))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
                //无限制 所有客户端都能访问
                //options.AddPolicy("LimitRequests", policy => 
                //policy.SetIsOriginAllowed((host) => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
        }
    }


}



