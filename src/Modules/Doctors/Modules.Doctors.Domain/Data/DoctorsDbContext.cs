using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;
using Modules.Doctors.Data.EntityModels.Doctor;
using SharedKernal.UnitOfWork;

namespace Modules.Doctors.Data
{
    internal sealed class DoctorsDbContext : DbContext
    {
        public DbSet<DoctorEntity> Agents => Set<DoctorEntity>();
        public DoctorsDbContext(IUOF unitOfWork, IEnumerable<IInterceptor>? interceptors = default) : base(CreateOptions(unitOfWork, interceptors))
        {
            unitOfWork.RegisterContext(this);
        }
        private static DbContextOptions CreateOptions(IUOF unitOfWork, IEnumerable<IInterceptor>? interceptors = default)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DoctorsDbContext>();

            optionsBuilder.UseSqlServer(unitOfWork.Connection, contextOwnsConnection: false, options =>
            {
                options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, ModuleConstants.Data.Schema);
            });

            return optionsBuilder.Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoctorsDbContext).Assembly);
            modelBuilder.HasDefaultSchema(ModuleConstants.Data.Schema);
        }
    }
}
