using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Clients.Data.EntityModels.Client;

namespace Modules.Clients.Data.DataMappings
{
    internal sealed class ClientEntityMapping : IEntityTypeConfiguration<ClientEntity>
    {
        public void Configure(EntityTypeBuilder<ClientEntity> builder)
        {
            builder.ToTable("Users", ModuleConstants.Data.Schema);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NameAr).HasMaxLength(50).IsRequired();
            builder.Property(x => x.NameEn).HasMaxLength(50).IsRequired();
        }
    }
}
