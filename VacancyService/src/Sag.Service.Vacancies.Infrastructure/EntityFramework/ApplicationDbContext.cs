using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Sag.Framework.EntityFramework.Configurations.Common;
using Sag.Framework.EntityFramework.Domain.Common;
using Sag.Framework.EntityFramework.Extensions;
using Sag.Framework.EntityFramework.SqlServer;
using Sag.Service.Vacancies.Domain;

namespace Sag.Service.Vacancies.Infrastructure.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Vacancy> Vacancies => Set<Vacancy>();
        public virtual DbSet<Company> Companies => Set<Company>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ReplaceService<IRelationalAnnotationProvider, SagSqlServerAnnotationProvider>()
                .ReplaceService<IMigrationsSqlGenerator, SagSqlServerMigrationsSqlGenerator>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AuditEntryConfig());

            modelBuilder.UseSagSoftDeleteFilters();
        }
    }
}
