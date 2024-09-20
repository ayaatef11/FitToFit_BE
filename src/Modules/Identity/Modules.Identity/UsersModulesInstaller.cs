using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Identity.ApiContracts;
using Modules.Identity.Data;
using Modules.Identity.Services.User;
using SharedKernal.Data;
using SharedKernal.ModuleInstaller;
using SharedKernal.Presistance.DbConnection;
using SharedKernal.UnitOfWork;
using System.Reflection;

namespace Modules.Identity
{
    internal class UsersModulesInstaller : IModuleInstaller
    {
        public Assembly InstallerAssemply => Assembly.GetExecutingAssembly();
        public Type RegisteredDbContextType => typeof(UsersDbContext);
        public IDatabaseInitializer GetDatabaseInitializer(IServiceProvider sp)
         => sp.GetRequiredKeyedService<IDatabaseInitializer>(nameof(UsersDbContext));
        public void InstallServices(IMvcBuilder mvcBuilder, IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(sp => new UsersDbContext(sp.GetRequiredService<IUOF>()));
            services.AddScoped<IUserService, UserApiService>();
            TypeAdapterConfig.GlobalSettings.Scan(InstallerAssemply);
            services.AddScoped<IDbConnectionFacory, UsersDbConnectionFactory>();
            mvcBuilder.AddApplicationPart(InstallerAssemply);

            services.AddKeyedTransient<IDatabaseInitializer, UsersDatabaseInitializer>(nameof(UsersDbContext));
        }
    }
}
