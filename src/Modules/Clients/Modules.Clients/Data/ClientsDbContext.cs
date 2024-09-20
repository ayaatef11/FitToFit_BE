using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;
using Modules.Clients.Data.EntityModels.Client;
using SharedKernal.UnitOfWork;

namespace Modules.Clients.Data
{
    internal sealed class ClientsDbContext : DbContext
    {
        public DbSet<ClientEntity> Clients => Set<ClientEntity>();
        public ClientsDbContext(IUOF unitOfWork, IEnumerable<IInterceptor>? interceptors = default) : base(CreateOptions(unitOfWork, interceptors))
        {
            unitOfWork.RegisterContext(this);
        }
        private static DbContextOptions CreateOptions(IUOF unitOfWork, IEnumerable<IInterceptor>? interceptors = default)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClientsDbContext>();

            optionsBuilder.UseSqlServer(unitOfWork.Connection, contextOwnsConnection: false, options =>
            {
                options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, ModuleConstants.Data.Schema);
            });

            return optionsBuilder.Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientsDbContext).Assembly);
            modelBuilder.HasDefaultSchema(ModuleConstants.Data.Schema);
        }
    }
}
