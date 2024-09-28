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
            //options to configure the database 
            var optionsBuilder = new DbContextOptionsBuilder<ClientsDbContext>();

            optionsBuilder.UseSqlServer(unitOfWork.Connection, contextOwnsConnection: false, options =>
            {
                //track the migration history table 
                // If you're using multiple DbContexts in a single database, it's helpful
                // to customize the migration history table for each context.
                // This prevents conflicts between different contexts and their migrations
                options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, ModuleConstants.Data.Schema);//as we  have multiple schemas in the same database and this is a logical way of separation 
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
