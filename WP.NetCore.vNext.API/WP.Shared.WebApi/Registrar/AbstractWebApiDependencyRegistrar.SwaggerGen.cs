using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.Core;
using WP.Infrastructures.JwtBearer;

namespace WP.Shared.WebApi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar
    {

        protected virtual void AddSwaggerGen()
        {
            var APIName = Appsettings.Get("APIName");
            var basePath = AppContext.BaseDirectory;
            Services.AddJwtAuthentication();
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(
                c =>
                {
                    typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                    {
                        c.SwaggerDoc(version, new OpenApiInfo
                        {
                            Version = version,
                            Title = $"{APIName}接口文档",
                            Description = $"{APIName} HTTP API {version}",
                        });
                        c.OrderActionsBy(o => o.RelativePath);
                    });
                    var xmlPath = Path.Combine(basePath, "WP.User.WebApi.xml");
                    c.IncludeXmlComments(xmlPath, true);// 开启加权小锁
                    c.OperationFilter<AddResponseHeadersFilter>();
                    c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                    c.OperationFilter<SecurityRequirementsOperationFilter>();
                    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "JWT授权 直接在下框中输入Bearer {token}",
                        Name = "Authorization",//jwt默认的参数名称
                        In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                        Type = SecuritySchemeType.ApiKey
                    });
                });
        }
    }
}
