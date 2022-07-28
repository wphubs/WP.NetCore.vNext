using MediatR;
using WP.Infrastructures.Core;
using WP.Infrastructures.EventBus.InMemory;
using WP.Infrastructures.SqlSugar;
using WP.Shared.Application;
using WP.User.Application.Interfaces;
using WP.User.Domain;
using WP.User.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WP.Infrastructures.JwtBearer;
using WP.User.Infrastruct;
using WP.Shared.WebApi;
using WP.Infrastructures.EventBus.InMemory.Events;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    //忽略循环引用
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    //设置时间格式
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    options.SerializerSettings.StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;
});
IConfiguration Configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.AddEnvironmentVariables()
.Build();

builder.Services.AddSingleton(new Appsettings(builder.Environment.ContentRootPath));

JWTSettingsOptions option = Configuration.GetSection("JWTSettings").Get<JWTSettingsOptions>();

var APIName = Appsettings.Get("APIName");
var basePath = AppContext.BaseDirectory;
builder.Services.AddJwtAuthentication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
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
        c.IncludeXmlComments(xmlPath, true);
        // 开启加权小锁
        c.OperationFilter<AddResponseHeadersFilter>();
        c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        // 在header中添加token，传递到后台
        c.OperationFilter<SecurityRequirementsOperationFilter>();
        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "JWT授权 直接在下框中输入Bearer {token}",
            Name = "Authorization",//jwt默认的参数名称
            In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
            Type = SecuritySchemeType.ApiKey
        });
    });
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddCorsPolicy();
builder.Services.AddScoped<IMediatorHandler, InMemoryBus>();
builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
builder.Services.AddScoped<IEventStoreService, SqlEventStore>();
IdGenerater.SetWorkerId(Appsettings.Get("WorkerId"));
builder.Services.AddSqlsugarSetup(new List<Type>() { typeof(SysUser), typeof(StoredEvent) });
builder.Services.AddScoped<ISqlSugarRepository, SqlSugarRepository>();
builder.Services.AddScoped(typeof(ISqlSugarRepository<>), typeof(SqlSugarRepository<>));
builder.Services.AddAssemblyDependency(typeof(IAccountAppService), typeof(UserCommandHandler), typeof(UserRepository));
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(Appsettings.Get("CorsAccessorSettings", "PolicyName"));
// 开启认证
app.UseAuthentication();
// 开启授权中间件
app.UseAuthorization();

app.MapControllers();
// 使用静态文件
app.UseStaticFiles();
// 使用cookie
app.UseCookiePolicy();
// 返回错误码
app.UseStatusCodePages();
// Routing
app.UseRouting();

app.Run();
