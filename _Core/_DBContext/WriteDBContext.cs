using BookStoreAPI.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI._Core._DBContext
{
    public class WriteDBContext : DbContext
    {
        public WriteDBContext()
        {
        }

        public WriteDBContext(DbContextOptions<ApplicationWriteContext> options)
            : base(options)
        {

        }

        protected virtual void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        }

        protected virtual void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
