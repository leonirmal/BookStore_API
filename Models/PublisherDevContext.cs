using System;
using System.Collections.Generic;
using BookStoreAPI.Domain.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Models;

public partial class PublisherDevContext : DbContext
{
    public PublisherDevContext()
    {
    }

    public PublisherDevContext(DbContextOptions<PublisherDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookStoreTbl> BookStoreDbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=L-ID-114;Initial Catalog=Publisher_Dev;User Id=L-ID-114\\Leo;TrustServerCertificate=True;Trusted_Connection=True");

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
