using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.Core.Interfaces;

namespace WP.Shared.Application.Registrar
{
    public abstract partial class AbstractApplicationDependencyRegistrar : IDependencyRegistrar
    {
        public AbstractApplicationDependencyRegistrar(IServiceCollection services)
        {

        }
        public string Name => "application";
        public abstract Assembly ApplicationLayerAssembly { get; }
        public abstract Assembly ContractsLayerAssembly { get; }
        public abstract Assembly RepositoryOrDomainLayerAssembly { get; }

        public abstract void AddAdnc();

        protected virtual void AddApplicaitonDefault()
        {
        }
    }
}
