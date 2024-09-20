using Microsoft.AspNetCore.Builder;
using SharedKernal.ModuleInstaller;

namespace SharedKernal.Data
{
    public static class Initializer
    {
        public static async Task CheckDefaultData(this WebApplication app)
        {
            foreach (var installer in ModularInstallerLoader.ModuleInstalelrs)
            {
                await installer.GetDatabaseInitializer(app.Services).SeedAndCheckDefaultData();
            }
        }
    }
}
