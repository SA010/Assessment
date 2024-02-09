using Sag.Framework.EntityFramework.Configurations;
using Sag.Service.Companies.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sag.Service.Companies.Infrastructure.EntityFramework.Configurations
{
    public class AddressConfig : EntityConfig<Address>, IEntityTypeConfiguration<Address>
    {
        public new void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            builder
                .Property(entity => entity.PostalCode)
                .HasMaxLength(10)
                .IsRequired();

            builder
                .Property(entity => entity.HouseNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(entity => entity.Affix)
                .HasMaxLength(10);

            builder
                .Property(entity => entity.LocationName)
                .HasMaxLength(100);

            builder
                .Property(entity => entity.Street)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(entity => entity.City)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .HasOne<ContactPerson>()
                .WithMany()
                .HasForeignKey(entity => entity.ContactPersonId);
        }
    }
}
