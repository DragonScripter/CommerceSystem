using CommerceDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceDAL
{
    public partial class CommerceContext : DbContext
    {
        public CommerceContext(DbContextOptions<CommerceContext> options) : base(options) 
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Stocks> Stocks { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Product>(entity => 
            {
                entity.HasKey(e => e.Id).HasName("PK_Product");
                entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
                entity.Property(e => e.Description)
               .HasMaxLength(1000)
               .IsUnicode(false);
                entity.Property(e => e.Price)
               .HasPrecision(18,2)
               .IsRequired();
                entity.Property(e => e.Timer)
               .IsRowVersion()
               .IsConcurrencyToken();
               entity.HasIndex(e => e.Name).IsUnique();
            });
            modelBuilder.Entity<Users>(entity => 
            {
                entity.HasKey(e => e.Id).HasName("PK_Users");
                entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
                entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
                entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
                entity.HasIndex(e => e.Email).IsUnique();
            });
            modelBuilder.Entity<Stocks>(entity => 
            {
                entity.HasKey(e => e.Id).HasName("PK_Stocks");
                entity.Property(e => e.Quanity)
                .HasColumnType("int")
                .IsRequired();

                entity.HasOne(d => d.Product).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductInStock");

            });
        }
    }
}
