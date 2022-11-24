using MediatR;
using WP.Infrastructures.Core;
using WP.Infrastructures.EventBus.InMemory;
using WP.Infrastructures.SqlSugar;
using WP.Shared.Application;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WP.Infrastructures.JwtBearer;
using WP.Shared.WebApi;
using WP.Infrastructures.EventBus.InMemory.Events;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using WP.User.Application.Services;
var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddControllers().AddNewtonsoftJson(options =>
//{
//    //����ѭ������
//    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
//    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
//    //����ʱ���ʽ
//    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
//    options.SerializerSettings.StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;
//});

//IConfiguration Configuration = new ConfigurationBuilder()
//.SetBasePath(Directory.GetCurrentDirectory())
//.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//.AddEnvironmentVariables()
//.Build();
//builder.Services.AddSingleton(new Appsettings(AppContext.BaseDirectory));
//JWTSettingsOptions option = Configuration.GetSection("JWTSettings").Get<JWTSettingsOptions>();
//var APIName = Appsettings.Get("APIName");
//var basePath = AppContext.BaseDirectory;
//builder.Services.AddJwtAuthentication();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(
//    c =>
//    {
//        typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
//        {
//            c.SwaggerDoc(version, new OpenApiInfo
//            {
//                Version = version,
//                Title = $"{APIName}�ӿ��ĵ�",
//                Description = $"{APIName} HTTP API {version}",
//            });
//            c.OrderActionsBy(o => o.RelativePath);
//        });
//        var xmlPath = Path.Combine(basePath, "WP.User.WebApi.xml");
//        c.IncludeXmlComments(xmlPath, true);// ������ȨС��
//        c.OperationFilter<AddResponseHeadersFilter>();
//        c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
//        c.OperationFilter<SecurityRequirementsOperationFilter>();
//        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
//        {
//            Description = "JWT��Ȩ ֱ�����¿�������Bearer {token}",
//            Name = "Authorization",//jwtĬ�ϵĲ�������
//            In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
//            Type = SecuritySchemeType.ApiKey
//        });
//    });

//builder.Services.AddMediatR(typeof(Program));
//builder.Services.AddScoped<IMediatorHandler, InMemoryBus>();
//builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
//builder.Services.AddScoped<IEventStoreService, SqlEventStore>();
IdGenerater.SetWorkerId(Appsettings.Get("WorkerId"));

builder.Services.AddSqlsugarSetup(Appsettings.Get("DBConnection"),new List<Type>() { typeof(SysUser), typeof(StoredEvent) });
builder.Services.AddScoped<ISqlSugarRepository, SqlSugarRepository>();
builder.Services.AddScoped(typeof(ISqlSugarRepository<>), typeof(SqlSugarRepository<>));
builder.Services.AddAssemblyDependency(typeof(AccountAppService));
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(Appsettings.Get("CorsAccessorSettings", "PolicyName"));
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
