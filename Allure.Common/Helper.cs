using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Common
{
    class Helper
    {
        public static Type[] GetRuntimeTypes()
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

            return assemblies.SelectMany(a => a.GetTypes()).ToArray();
        }

        private static bool IsSystemAssembly(Assembly assembly)
        {
            return assembly == null
                || assembly.FullName.StartsWith("mscorlib.")
                || assembly.FullName.StartsWith("System.")
                || assembly.FullName.StartsWith("Microsoft.");
        }
    }
}
