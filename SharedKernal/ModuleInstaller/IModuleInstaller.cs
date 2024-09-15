using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SharedKernal.ModuleInstaller
{
    public interface IModuleInstaller
    {
        Assembly InstallerAssemply { get; }
        Type RegisteredDbContextType { get; }
        void InstallServices(IMvcBuilder mvcBuilder, IServiceCollection services, IConfiguration configuration);
    }
}
