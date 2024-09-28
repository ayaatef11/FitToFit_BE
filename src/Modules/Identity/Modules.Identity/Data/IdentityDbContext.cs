using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;
using Modules.Identity;
using Modules.Identity.Data.EntityModels;
using Modules.Identity.Data.Enums;
using SharedKernal.UnitOfWork;

namespace Modules.Identity.Data
{
    //how they are of the same name the child and the parent?
    internal sealed class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
         IdentityRoleClaim<string>,
         IdentityUserToken<string>>
    {
        public IdentityDbContext(IUOF unitOfWork, IEnumerable<IInterceptor>? interceptors = default) : base(CreateOptions(unitOfWork, interceptors))
        {
            unitOfWork.RegisterContext(this);
        }

        public DbSet<UserTypeInfo> UserTypeInfos => Set<UserTypeInfo>();

        private static DbContextOptions CreateOptions(IUOF unitOfWork, IEnumerable<IInterceptor>? interceptors = default)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();

            optionsBuilder.UseSqlServer(unitOfWork.Connection, contextOwnsConnection: false, options =>
            {
                options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, ModuleConstants.Data.Schema);
            });

            return optionsBuilder.Options;
        }

        private void ConfigureAuidtableEntities(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditable).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).Property(nameof(IAuditable.CreatedBy))
                        .HasMaxLength(450)
                        .IsRequired(false);

                    modelBuilder.Entity(entityType.ClrType).Property(nameof(IAuditable.LastModifiedBy))
                        .HasMaxLength(450)
                        .IsRequired(false);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set the schema for all tables in the model
            modelBuilder.HasDefaultSchema(ModuleConstants.Data.Schema);

            ConfigureAuidtableEntities(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);

            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        }

        public async Task SeedUserTypesAsync(CancellationToken token)
        {
            var userTypes = UserType.GetAll();

            // Fetch existing UserTypeInfo records from the database
            var existingUserTypes = await UserTypeInfos
                .ToDictionaryAsync(uti => uti.Id);

            foreach (var userType in userTypes)
            {
                // If a UserTypeInfo does not exist in the database, insert it
                if (!existingUserTypes.ContainsKey(userType.Key))
                {
                    var newUserTypeInfo = UserTypeInfo.Create(userType.Key);

                    await UserTypeInfos.AddAsync(newUserTypeInfo, token);
                }
            }

            await SaveChangesAsync(token);
        }
    }
}
