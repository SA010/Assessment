using Sag.Framework.EntityFramework.Configurations;
using Sag.Service.Vacancies.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sag.Service.Vacancies.Infrastructure.EntityFramework.Configurations
{
    public class CompanyConfig : EntityConfig<Company>, IEntityTypeConfiguration<Company>
    {
        public new void Configure(EntityTypeBuilder<Company> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name)
                .HasMaxLength(250);

            builder.Property(c => c.DisplayName)
                .HasMaxLength(250);
        }    
    }
}
