using Sag.Framework.EntityFramework.Configurations;
using Sag.Service.Companies.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sag.Service.Companies.Infrastructure.EntityFramework.Configurations
{
    public class ContactPersonConfig : EntityConfig<ContactPerson>, IEntityTypeConfiguration<ContactPerson>
    {
        public new void Configure(EntityTypeBuilder<ContactPerson> builder)
        {
            base.Configure(builder);

            builder
                .Property(entity => entity.FirstName)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(entity => entity.Preposition)
                .HasMaxLength(250);

            builder
                .Property(entity => entity.LastName)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(entity => entity.EmailAddress)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(entity => entity.PhoneNumber)
                .HasMaxLength(25);

            builder
                .Property(entity => entity.FunctionTitle)
                .HasMaxLength(250);

            builder
                .Property(entity => entity.IsMainContactPerson)
                .IsRequired();
        }
    }
}
