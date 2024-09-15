using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedKernal.MediatR.Piplines;
using SharedKernal.ModuleInstaller;

namespace SharedKernal.MediatR
{
    public static class MediatRInstaller
    {
        public static void AddAppMediaR(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(ModularInstallerLoader.GetModulesAssemblies());
            });
        }
    }
}
