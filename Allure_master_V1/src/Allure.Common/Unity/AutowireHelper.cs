using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Common.Unity
{
    static class AutowireHelper
    {
        public static Assembly[] GetAssemblies()
        {
            var path = AppDomain.CurrentDomain.RelativeSearchPath;
            if (string.IsNullOrWhiteSpace(path))
            {
                path = AppDomain.CurrentDomain.BaseDirectory;
            }

            var assemblies = new List<Assembly>();

            foreach (var file in Directory.EnumerateFiles(path, "*.dll"))
            {
                try
                {
                    var assembly = Assembly.Load(Path.GetFileNameWithoutExtension(file));

                    if (!IsSystemAssembly(assembly))
                    {
                        assemblies.Add(assembly);
                    }
                }
                catch { }
            }

            return assemblies.ToArray();
        }

        private static bool IsSystemAssembly(Assembly assembly)
        {
            return assembly == null
                || assembly.FullName.StartsWith("mscorlib.")
                || assembly.FullName.StartsWith("System.")
                || assembly.FullName.StartsWith("Microsoft.");
        }

        public static LifetimeManager CreateLifetimeManager(IUnityContainer container, Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.PerThread:
                    return container.Resolve<PerThreadLifetimeManager>();

                case Lifetime.Singleton:
                    return container.Resolve<ContainerControlledLifetimeManager>();
                    
                case Lifetime.Transient:
                default:
                    return container.Resolve<TransientLifetimeManager>();
            }
        }
    }
}
