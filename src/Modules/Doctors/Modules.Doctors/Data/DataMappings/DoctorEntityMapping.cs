using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Doctors.Data.EntityModels.Doctor;

namespace Modules.Doctors.Data.DataMappings
{
    internal sealed class DoctorEntityMapping : IEntityTypeConfiguration<DoctorEntity>
    {
        public void Configure(EntityTypeBuilder<DoctorEntity> builder)
        {
            builder.ToTable("Users", ModuleConstants.Data.Schema);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DrNameAr).HasMaxLength(50).IsRequired();
            builder.Property(x => x.DrNameEn).HasMaxLength(50).IsRequired();
        }
    }
}
