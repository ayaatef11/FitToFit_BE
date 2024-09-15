using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Clients.ApiContracts.Client;
using Modules.Clients.Data;
using Modules.Clients.Services.Client;
using SharedKernal.ModuleInstaller;
using SharedKernal.Presistance.DbConnection;
using SharedKernal.UnitOfWork;
using System.Reflection;

namespace Modules.Clients
{
    internal class ClientsModuleInstaller : IModuleInstaller
    {
        public Assembly InstallerAssemply => Assembly.GetExecutingAssembly();

        public Type RegisteredDbContextType => typeof(ClientsDbContext);

        public void InstallServices(IMvcBuilder mvcBuilder, IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(sp => new ClientsDbContext(sp.GetRequiredService<IUOF>()));
            services.AddScoped<IClientsService, ClientsApiService>();
            TypeAdapterConfig.GlobalSettings.Scan(InstallerAssemply);
            services.AddScoped<IDbConnectionFacory, ClientDbConnectionFactory>();
            mvcBuilder.AddApplicationPart(InstallerAssemply);
        }
    }
}
