using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Identity.Data.EntityModels;

namespace Modules.Identity.Data.DataMappings
{
    public sealed class RoleEntityConfig : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(x => x.Id);
        }
    }
}
