using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Infrastructures.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class UseDependencyInjectionAttribute : Attribute
    {
        public ServiceLifetime lifetime;

        public UseDependencyInjectionAttribute(ServiceLifetime argLifetime = ServiceLifetime.Scoped)
        {
            lifetime = argLifetime;
        }

        public ServiceLifetime Lifetime
        {
            get
            {
                return this.lifetime;
            }
        }
    }
}
