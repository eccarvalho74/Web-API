using Certificados.Domain.Core.i18n;
using Microsoft.Extensions.Localization;

namespace Certificados.Infra.i18n
{
   public class GlobalizationResource : IGlobalizationResource
    {
        private readonly IStringLocalizer stringLocalizer;

        public string this[string key, params object[] arguments] => GetText(key, arguments);

        public GlobalizationResource(IStringLocalizerFactory factory)
        {
            var type = typeof(GlobalizationResource);
            this.stringLocalizer = factory.Create(type.Name, type.Assembly.GetName().Name);
        }

        public static string GetResourceNamespacePath()
        {
            var type = typeof(GlobalizationResource);
            var assemblyName = type.Assembly.GetName().Name;
            var path = type.Namespace.Remove(0, assemblyName.Length + 1);

            return path;
        }

        private string GetText(string key, params object[] arguments)
        {
            var texto = stringLocalizer[key, arguments];

            if (texto.ResourceNotFound)
                return null;

            return texto;
        }
    }
}
