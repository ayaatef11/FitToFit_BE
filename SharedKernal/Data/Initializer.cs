using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharedKernal.ModuleInstaller;

namespace SharedKernal.Data
{
    public static class Initializer 
    {
        public static async Task CheckDefaultData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            foreach (var installer in ModularInstallerLoader.ModuleInstalelrs)
            {
                await installer.GetDatabaseInitializer(scope.ServiceProvider).SeedAndCheckDefaultData();
            }
        }
    }
}
