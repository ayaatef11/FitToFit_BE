using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Identity.Data.EntityModels;

namespace Modules.Identity.Data.DataMappings
{
    public sealed class UserTypeInfoConfig : IEntityTypeConfiguration<UserTypeInfo>
    {
        public void Configure(EntityTypeBuilder<UserTypeInfo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NameEn)
            .HasMaxLength(60)
            .IsRequired();

            builder.Property(x => x.NameAr)
            .HasMaxLength(60)
            .IsRequired();
        }
    }

}
