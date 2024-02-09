using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Sag.Framework.EntityFramework.Configurations.Common;
using Sag.Framework.EntityFramework.Domain.Common;
using Sag.Framework.EntityFramework.Extensions;
using Sag.Framework.EntityFramework.SqlServer;
using Sag.Service.Companies.Domain;

namespace Sag.Service.Companies.Infrastructure.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Company> Companies => Set<Company>();
        public virtual DbSet<Address> Addresses => Set<Address>();
        public virtual DbSet<ContactPerson> ContactPersons => Set<ContactPerson>();

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
