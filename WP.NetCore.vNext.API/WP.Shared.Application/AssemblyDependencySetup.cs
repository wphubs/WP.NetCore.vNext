using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.Core;
namespace WP.Shared.Application
{
    public static class AssemblyDependencySetup
    {

        public static void AddAssemblyDependency(this IServiceCollection services, Type type)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            //if (assemblyName.IsNullOrEmpty()) throw new ArgumentNullException(nameof(assemblyName));
            //Assembly assembly = Assembly.Load(assemblyName);
            List<Type> ts = type.Assembly.GetExportedTypes().Where(t => !t.IsInterface && !t.IsSealed && !t.IsGenericType).ToList();
            foreach (var classitem in ts)
            {
                IEnumerable<Attribute> attrs = classitem.GetCustomAttributes();
                foreach (var at in attrs)
                {
                    if (at is UseDependencyInjectionAttribute)
                    {
                        UseDependencyInjectionAttribute attr = classitem.GetCustomAttribute(typeof(UseDependencyInjectionAttribute)) as UseDependencyInjectionAttribute;
                        List<Type> interfaceType = classitem.GetInterfaces().ToList();
                        if (interfaceType.Count == 0)
                        {
                            Type baseType = classitem.BaseType;
                            while (baseType != null)
                            {
                                if (baseType.IsAbstract)
                                {
                                    ServiceDescriptor serviceDescriptor = new ServiceDescriptor(baseType, classitem, attr.Lifetime);
                                    services.Add(serviceDescriptor);
                                    break;
                                }
                                else
                                {
                                    baseType = baseType.BaseType;
                                }
                            }
                        }
                        else
                        {
                            foreach (Type faceitem in interfaceType)
                            {
                                ServiceDescriptor serviceDescriptor = new ServiceDescriptor(faceitem, classitem, attr.Lifetime);
                                services.Add(serviceDescriptor);
                            }
                        }
                    }
                }
            }
        }
    }
}
