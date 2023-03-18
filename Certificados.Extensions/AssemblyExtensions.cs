using System.Reflection;

namespace Certificados.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetNoAbstractTypes(this Assembly assemblyFile)
        {
            var types = assemblyFile.GetTypes();
            var assemblyTypes = types.Where(t => !t.GetTypeInfo().IsAbstract);

            return assemblyTypes;
        }
    }
}
