using Certificados.Domain.Core.i18n;
using Certificados.Domain.Core.Interfaces;
using Certificados.Extensions;
using Certificados.Infra.Core.UoW;
using Certificados.Infra.i18n;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Certificados.Infra.Core.Initialization
{
    public static class DependencyInjectionServices
    {
        public static void AddInfraDependencies(this IServiceCollection services)
        {
            services.AddUnitOfWork();
            services.AddGlobalization();
            services.AddRepositories();

            //Mapeamento realizado por Reflection.
            //Basta que a interface do repositório herde de IBaseRepository
            //services.AddScoped<IDummyRepository, DummyRepository>();
            ;
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            IEnumerable<Type> assemblyTypes = typeof(DependencyInjectionServices).Assembly.GetNoAbstractTypes();
            services.AddImplementations(ServiceLifetime.Scoped, typeof(IBaseRepository), assemblyTypes);
        }

        private static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddGlobalization(this IServiceCollection services)
        {
            services.AddScoped<IGlobalizationResource, GlobalizationResource>();

            services.AddLocalization(o => o.ResourcesPath = GlobalizationResource.GetResourceNamespacePath());
            services.Configure<RequestLocalizationOptions>(o =>
            {
                var supportedCultures = new[] {
                    new CultureInfo("pt-BR"),
                    new CultureInfo("en-US")
                };

                o.SupportedCultures = supportedCultures;
                o.SupportedUICultures = supportedCultures;

                o.FallBackToParentCultures = true;
                o.FallBackToParentUICultures = true;

                o.RequestCultureProviders = new IRequestCultureProvider[] {
                    new AcceptLanguageHeaderRequestCultureProvider()
                };
            });
        }
    }
}
