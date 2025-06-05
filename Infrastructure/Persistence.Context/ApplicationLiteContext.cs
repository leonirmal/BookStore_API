using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Infrastructure.Persistence.Context
{
    public partial class ApplicationAuditLogContext : DbContext
    {
        public DbSet<ServiceAuditLog> ServiceAuditLogs { get; set; }

        public DbSet<EntityAuditLog> EntityAuditLogs { get; set; }

        public ApplicationAuditLogContext()
        {

        }

        public ApplicationAuditLogContext(DbContextOptions<ApplicationAuditLogContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }

    public class ServiceAuditLog
    {
        public int Id { get; set; }
        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string RequestData { get; set; }

        public string ResponseData { get; set; }

        public DateTime CreatedOn { get { return DateTime.UtcNow; } }

        public long CreatedBy { get; set; }
    }

    public class EntityAuditLog
    {
        public int Id { get; set; }
        public string EntityName { get; set; }

        public string EntityKey { get; set; }

        public string ActionName { get; set; }

        public string RequestData { get; set; }

        public string ResponseData { get; set; }

        public DateTime CreatedOn { get { return DateTime.UtcNow; } }

        public long CreatedBy { get; set; }
    }
}
