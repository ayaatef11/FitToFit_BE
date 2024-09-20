using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Identity.Data.EntityModels;

namespace Modules.Identity.Data.DataMappings
{
    public sealed class UserRoleEntityConfig : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable("UserRoles");

            builder.HasOne(a => a.Role)
                .WithMany()
                .HasForeignKey(a => a.RoleId);
        }
    }
}
