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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
IConfiguration Configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.AddEnvironmentVariables()
.Build();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddScoped<IMediatorHandler, InMemoryBus>();
IdGenerater.SetWorkerId(Appsettings.Get("WorkerId"));
builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
builder.Services.AddSingleton(new Appsettings(builder.Environment.ContentRootPath));
builder.Services.AddSqlsugarSetup(typeof(SysUser));
builder.Services.AddScoped<ISqlSugarRepository, SqlSugarRepository>();
builder.Services.AddScoped(typeof(ISqlSugarRepository<>), typeof(SqlSugarRepository<>));
builder.Services.AddAssemblyDependency(typeof(IAccountAppService));
builder.Services.AddAssemblyDependency(typeof(UserCommandHandler));
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();

app.Run();
