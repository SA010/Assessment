using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Sag.Framework.EntityFramework.Configurations.Common;
using Sag.Framework.EntityFramework.Domain.Common;
using Sag.Framework.EntityFramework.Extensions;
using Sag.Framework.EntityFramework.SqlServer;

namespace Sag.SampleBFF.Api.Infrastructure.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<AuditEntry> AuditEntries => Set<AuditEntry>();

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
