using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Certificados.Extensions
{
    public static class DIServicesExtensions
    {
        public static void AddImplementations(this IServiceCollection services,
            ServiceLifetime serviceLifetime, Type baseInterface, IEnumerable<Type> assemblyTypesParam = null)
        {
            var assemblyTypes = assemblyTypesParam ?? baseInterface.Assembly.GetNoAbstractTypes();

            foreach (var assemblyType in assemblyTypes)
            {
                var baseInterfaceType = baseInterface;
                var typeInterfaces = assemblyType.GetInterfaces();
                var implementsInterface = typeInterfaces
                    .Any(p => p.AssemblyQualifiedName == baseInterfaceType.AssemblyQualifiedName);

                if (implementsInterface)
                {
                    var typeInterface = typeInterfaces
                        .FirstOrDefault(t => t.GetInterfaces().Contains(baseInterfaceType));

                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(typeInterface, assemblyType);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(typeInterface, assemblyType);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(typeInterface, assemblyType);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}