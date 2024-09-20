using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Reflection;

namespace SharedKernal.ModuleInstaller
{
    public static class ModularInstallerLoader
    {
        private static ICollection<IModuleInstaller> _modulesInstalelrs = null;
        public static ICollection<IModuleInstaller> ModuleInstalelrs
        {
            get
            {
                if (_modulesInstalelrs is null)
                    _modulesInstalelrs = GetModuleInstallers().ToList();

                return _modulesInstalelrs;
            }
        }
        public static Assembly[] GetModulesAssemblies()
            => ModuleInstalelrs.Select(i => i.InstallerAssemply).ToArray();
        private static IEnumerable<IModuleInstaller> GetModuleInstallers()
        {
            var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory)
                 .Where(
                    f => Path.GetExtension(f) == ".dll" &&
                    Path.GetFileName(f).StartsWith("Modules.", StringComparison.OrdinalIgnoreCase))
                 .Select(Assembly.LoadFrom).ToList();

            var installers = assemblies
            .SelectMany(assembly => assembly.GetTypes())  // Get all types from each assembly
            .Where(type => typeof(IModuleInstaller).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            .Select(Activator.CreateInstance)  // Create instances of the types
            .Cast<IModuleInstaller>();

            return installers;
        }

        public static InstallResult InstallModules(this IServiceCollection services, IMvcBuilder mvcBuilder, IConfiguration configuration)
        {
            var dbs = new Collection<Type>();

            foreach (var installer in ModuleInstalelrs)
            {
                // Log or output the module being installed
                Console.WriteLine($"Installing module: {installer.GetType().Name}");

                // Call each module's InstallServices method
                installer.InstallServices(mvcBuilder, services, configuration);
                dbs.Add(installer.RegisteredDbContextType);
            }

            return new InstallResult
            {
                RegisteredDatabases = dbs
            };
        }
    }
}
