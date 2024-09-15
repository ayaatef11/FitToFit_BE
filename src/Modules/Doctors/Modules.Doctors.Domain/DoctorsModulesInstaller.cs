using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Doctors.ApiContracts.Doctor;
using Modules.Doctors.Data;
using Modules.Doctors.Services.Doctor;
using SharedKernal.ModuleInstaller;
using SharedKernal.Presistance.DbConnection;
using SharedKernal.UnitOfWork;
using System.Reflection;

namespace Modules.Doctors
{
    internal class DoctorsModulesInstaller : IModuleInstaller
    {
        public Assembly InstallerAssemply => Assembly.GetExecutingAssembly();
        public Type RegisteredDbContextType => typeof(DoctorsDbContext);
        public void InstallServices(IMvcBuilder mvcBuilder, IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(sp => new DoctorsDbContext(sp.GetRequiredService<IUOF>()));
            services.AddScoped<IDoctorService, DoctorsApiService>();
            TypeAdapterConfig.GlobalSettings.Scan(InstallerAssemply);
            services.AddScoped<IDbConnectionFacory, DoctorsDbConnectionFactory>();
            mvcBuilder.AddApplicationPart(InstallerAssemply);
        }
    }
}
