using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hearts.Reflection
{
    public static class AppDomainExtensions
    {
        public static IEnumerable<Type> ResolveInterfaceImplementations<T>(this AppDomain self, bool includeAdjacentDllsNotDirectlyLoaded = false)
        {
            if (includeAdjacentDllsNotDirectlyLoaded)
            {
                var allAssemblies = new List<Assembly>();
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                foreach (string dll in Directory.GetFiles(path, "*.dll"))
                {
                    var assembley = Assembly.LoadFile(dll);
                    allAssemblies.Add(assembley);
                }
            }
            
            var type = typeof(T);
            return self.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));
        } 
    }
}
