using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WP.Shared.Application.Registrar;
using WP.User.Repository;

namespace WP.User.Application.Registrar
{
    public class UserApplicationDependencyRegistrar: AbstractApplicationDependencyRegistrar
    {
        public override Assembly ApplicationLayerAssembly => Assembly.GetExecutingAssembly();

        public override Assembly ContractsLayerAssembly => typeof(IAccountAppService).Assembly;

        public override Assembly RepositoryOrDomainLayerAssembly => typeof(EntityAssemblyInfo).Assembly;

        public UserApplicationDependencyRegistrar(IServiceCollection services) : base(services)
        {
        }

        public override void AddAdnc() => AddApplicaitonDefault();


    }
}
