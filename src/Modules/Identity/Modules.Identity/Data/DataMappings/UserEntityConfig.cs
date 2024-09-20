using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Identity.Data.EntityModels;

namespace Modules.Identity.Data.DataMappings
{
    public class UserEntityConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            builder.HasOne(x => x.UserTypeInfo)
              .WithMany()
              .HasForeignKey(x => x.UserTypeInfoId);

            builder.Navigation(x => x.UserTypeInfo).AutoInclude();

            builder.Navigation(x => x.UserRoles).AutoInclude();

            builder.Property(x => x.NationalId)
            .HasMaxLength(10)
            .IsRequired();

            builder.Property(x => x.FullNameEn)
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(x => x.FullNameAr)
              .HasMaxLength(100)
              .IsRequired();


            builder.Property(x => x.BirthDate)
            .HasMaxLength(30)
            .IsRequired();


            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasMany(e => e.UserRoles)
              .WithOne() // Since Identity does not expose navigation properties in IdentityRole
              .HasForeignKey(ur => ur.UserId)
              .IsRequired();
        }
    }

}
