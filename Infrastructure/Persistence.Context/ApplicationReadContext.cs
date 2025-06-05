using BookStoreAPI._Core._DBContext;
using BookStoreAPI.Domain.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Infrastructure.Persistence.Context
{
    public partial class ApplicationReadContext : ReadDBContext
    {
        public virtual DbSet<BookStoreTbl> Books { get; set; }
        public ApplicationReadContext()
        {
        }

        public ApplicationReadContext(DbContextOptions<ApplicationReadContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookStoreTbl>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__BookStor__3214EC07AB153B7F");

                entity.ToTable("BookStoreDB");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.AuthorFirstName).HasMaxLength(255);
                entity.Property(e => e.AuthorLastName).HasMaxLength(255);
                entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");
                entity.Property(e => e.Publisher).HasMaxLength(255);
                entity.Property(e => e.Title).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
