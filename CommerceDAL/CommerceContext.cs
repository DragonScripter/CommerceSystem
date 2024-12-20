﻿using CommerceDAL.Entities;
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
        public CommerceContext() { }
        public CommerceContext(DbContextOptions<CommerceContext> options) : base(options) 
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Stocks> Stocks { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
                optionsBuilder.UseSqlServer(connectionString);
            }
            optionsBuilder.UseLazyLoadingProxies();
        }

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
                entity.Property(e => e.Timer)
                .IsRowVersion()
                .IsConcurrencyToken();
                entity.HasIndex(e => e.Email).IsUnique();

            });
            modelBuilder.Entity<Stocks>(entity => 
            {
                entity.HasKey(e => e.Id).HasName("PK_Stocks");
                entity.Property(e => e.Quanity)
                .HasColumnType("int")
                .IsRequired();
                entity.Property(e => e.Timer)
                .IsRowVersion()
                .IsConcurrencyToken();

                entity.HasOne(d => d.Product).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductInStock");

            });
            modelBuilder.Entity<Orders>(entity => 
            {
                entity.HasKey(e => e.Id).HasName("PK_Orders");
                entity.Property(e => e.OrderStatus)
                .HasMaxLength(100)
                .IsUnicode(false);
                entity.Property(e => e.Date).HasColumnType("smalldatetime");
                entity.Property(e => e.Timer)
               .IsRowVersion()
               .IsConcurrencyToken();

                entity.HasOne(o => o.Users).WithMany(u => u.UsersOrders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersHasOrders");

                entity.HasOne(o => o.Product).WithMany(p => p.ProductOrders)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductHasOrders");

                entity.HasOne(o => o.Stocks).WithMany(P => P.StocksOrders)
                .HasForeignKey(o => o.StockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StocksHasOrders");

            });


        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}