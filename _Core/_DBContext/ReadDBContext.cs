using BookStoreAPI.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI._Core._DBContext
{
    public class ReadDBContext : DbContext
    {
        public ReadDBContext()
        {
        }

        public ReadDBContext(DbContextOptions<ApplicationReadContext> options)
            : base(options)
        {

        }

        protected virtual void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected virtual void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
