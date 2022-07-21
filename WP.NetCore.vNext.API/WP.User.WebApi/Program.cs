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

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    //����ѭ������
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    //����ʱ���ʽ
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    options.SerializerSettings.StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;
});
IConfiguration Configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.AddEnvironmentVariables()
.Build();


JWTSettingsOptions option = Configuration.GetSection("JWTSettings").Get<JWTSettingsOptions>();


builder.Services.AddSingleton(new Appsettings(builder.Environment.ContentRootPath));
builder.Services.AddJwtAuthentication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddScoped<IMediatorHandler, InMemoryBus>();
builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
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
// ������֤
app.UseAuthentication();
// ������Ȩ�м��
app.UseAuthorization();

app.MapControllers();
// ʹ�þ�̬�ļ�
app.UseStaticFiles();
// ʹ��cookie
app.UseCookiePolicy();
// ���ش�����
app.UseStatusCodePages();
// Routing
app.UseRouting();

app.Run();
