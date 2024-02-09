using Sag.Framework.EntityFramework.Configurations;
using Sag.Service.Companies.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sag.Service.Companies.Infrastructure.EntityFramework.Configurations
{
    public class CompanyConfig : EntityConfig<Company>, IEntityTypeConfiguration<Company>
    {
        public new void Configure(EntityTypeBuilder<Company> builder)
        {
            base.Configure(builder);

            builder
                .Property(entity => entity.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(entity => entity.EmailAddress)
                .HasMaxLength(250);

            builder
                .Property(entity => entity.PhoneNumber)
                .HasMaxLength(250);

            builder.HasMany(entity => entity.ContactPersons);
            builder.HasMany(entity => entity.Addresses);
        }
    }
}
