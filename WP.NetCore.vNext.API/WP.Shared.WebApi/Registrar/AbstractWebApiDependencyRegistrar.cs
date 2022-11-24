using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.Core;
using WP.Infrastructures.Core.DependencyInjection;
using WP.Infrastructures.Core.Interfaces;
namespace WP.Shared.WebApi.Registrar;

public abstract partial class AbstractWebApiDependencyRegistrar : IDependencyRegistrar
{
    public string Name => "webapi";

    public abstract void AddAdnc();
    protected IConfiguration Configuration { get; init; }
    protected IServiceCollection Services { get; init; }
    //protected IServiceInfo ServiceInfo { get; init; }


    protected AbstractWebApiDependencyRegistrar(IServiceCollection services)
    {
        Services = services;
        //Configuration = services.GetConfiguration();
        Configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddEnvironmentVariables()
          .Build();
    }



    protected virtual void AddWebApiDefault()
    {
        Services.AddSingleton(new Appsettings(AppContext.BaseDirectory));
        AddControllers();
        AddCorsPolicy();
        AddSwaggerGen();
    }
}
