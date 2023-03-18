using Certificados.Extensions;
using Certificados.Services.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Certificados.Services.Core.Initialization
{
    public static class DependencyInjectionServices
    {
        public static void AddDomainServicesDependencies(this IServiceCollection services)
        {
            services.AddServices();
            services.AddValidators();

            //Mapeamento realizado por Reflection.
            //Basta que a interface do repositório herde de IBaseService ou IBaseValidator
            //services.AddTransient<IDummyValidator, DummyValidator>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddImplementations(ServiceLifetime.Transient, typeof(IBaseService));
        }

        private static void AddValidators(this IServiceCollection services)
        {
           // services.AddImplementations(ServiceLifetime.Transient, typeof(IBaseValidator));
        }
    }
}
