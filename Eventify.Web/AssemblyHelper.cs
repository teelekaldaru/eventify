using Eventify.Common.Classes.Exceptions;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Loader;

namespace Eventify.Web
{
    public static class AssemblyHelper
    {
        public static Assembly[] GetAssemblies()
        {
            var assemblies = new List<Assembly>();
            foreach (ProcessModule module in Process.GetCurrentProcess().Modules)
            {
                try
                {
                    var assemblyName = AssemblyLoadContext.GetAssemblyName(module.FileName ?? throw new SimpleException("ProcessModule name not found"));
                    if (!assemblyName.FullName.Contains("Eventify."))
                    {
                        continue;
                    }

                    var assembly = Assembly.Load(assemblyName);
                    assemblies.Add(assembly);
                }
                catch (Exception)
                {
                    // ignore native modules
                }
            }

            return assemblies.ToArray();
        }

        public static string GetAppVersion(Assembly assembly)
        {
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion ?? throw new SimpleException("App version not specified");
        }
    }
}
