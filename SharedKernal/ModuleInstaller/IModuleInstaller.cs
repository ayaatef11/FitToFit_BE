using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernal.Data;
using System.Reflection;

namespace SharedKernal.ModuleInstaller
{
    public interface IModuleInstaller
    {
        IDatabaseInitializer GetDatabaseInitializer(IServiceProvider sp);
        Assembly InstallerAssemply { get; }
        Type RegisteredDbContextType { get; }
        void InstallServices(IMvcBuilder mvcBuilder, IServiceCollection services, IConfiguration configuration);
    }
}
