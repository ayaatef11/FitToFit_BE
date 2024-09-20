using ApisContracts.Identity;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Identity.Data;
using Modules.Identity.Services.User;
using SharedKernal.Data;
using SharedKernal.ModuleInstaller;
using SharedKernal.Presistance.DbConnection;
using SharedKernal.UnitOfWork;
using System.Reflection;

namespace Modules.Identity
{
    internal class IdentityModulesInstaller : IModuleInstaller
    {
        public Assembly InstallerAssemply => Assembly.GetExecutingAssembly();
        public Type RegisteredDbContextType => typeof(IdentityDbContext);
        public IDatabaseInitializer GetDatabaseInitializer(IServiceProvider sp)
         => sp.GetRequiredKeyedService<IDatabaseInitializer>(nameof(IdentityDbContext));
        public void InstallServices(IMvcBuilder mvcBuilder, IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(sp => new IdentityDbContext(sp.GetRequiredService<IUOF>()));
            services.AddScoped<IIdentityModuleService, IdentityModuleService>();
            TypeAdapterConfig.GlobalSettings.Scan(InstallerAssemply);
            services.AddScoped<IDbConnectionFacory, IdentityDbConnectionFactory>();
            mvcBuilder.AddApplicationPart(InstallerAssemply);

            services.AddKeyedTransient<IDatabaseInitializer, IdentityDatabaseInitializer>(nameof(IdentityDbContext));
        }
    }
}
