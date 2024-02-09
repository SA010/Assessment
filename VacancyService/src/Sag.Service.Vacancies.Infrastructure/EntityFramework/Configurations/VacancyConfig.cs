using Sag.Framework.EntityFramework.Configurations;
using Sag.Framework.EntityFramework.Extensions;
using Sag.Service.Vacancies.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sag.Service.Vacancies.Infrastructure.EntityFramework.Configurations
{
    internal class VacancyConfig : EntityConfig<Vacancy>, IEntityTypeConfiguration<Vacancy>
    {
        public new void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            base.Configure(builder);

            builder.Property(v => v.Title)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(v => v.FunctionTitle)
                .HasMaxLength(250);

            builder.Property(v => v.Location)
                .HasMaxLength(250);

            builder
                .Property(v => v.JobDescription)
                .HasMaxLength(4000);

            builder
                .HasOne(v => v.Company)
                .WithMany()
                .HasForeignKey(v => v.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
