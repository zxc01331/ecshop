using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eshop.Models;

public partial class eshopContext : DbContext
{
    public eshopContext(DbContextOptions<eshopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<brands> brands { get; set; }

    public virtual DbSet<cart_items> cart_items { get; set; }

    public virtual DbSet<categories> categories { get; set; }

    public virtual DbSet<order_items> order_items { get; set; }

    public virtual DbSet<orders> orders { get; set; }

    public virtual DbSet<product_images> product_images { get; set; }

    public virtual DbSet<products> products { get; set; }

    public virtual DbSet<products_cart_items> products_cart_items { get; set; }

    public virtual DbSet<user_profiles> user_profiles { get; set; }

    public virtual DbSet<users> users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<brands>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.brand_name).HasMaxLength(50);
            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.updated_at).HasColumnType("timestamp");
        });

        modelBuilder.Entity<cart_items>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.user_id, "user_id");

            entity.Property(e => e.added_at).HasColumnType("timestamp");

            entity.HasOne(d => d.user).WithMany(p => p.cart_items)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("cart_items_ibfk_1");
        });

        modelBuilder.Entity<categories>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.category_name).HasMaxLength(50);
            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.updated_at).HasColumnType("timestamp");
        });

        modelBuilder.Entity<order_items>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<orders>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.create_at).HasColumnType("timestamp");
            entity.Property(e => e.payment_method).HasMaxLength(32);
            entity.Property(e => e.shipping_address).HasMaxLength(255);
            entity.Property(e => e.status).HasMaxLength(32);
        });

        modelBuilder.Entity<product_images>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.product_id, "product_id");

            entity.Property(e => e.image_url).HasMaxLength(255);

            entity.HasOne(d => d.product).WithMany(p => p.product_images)
                .HasForeignKey(d => d.product_id)
                .HasConstraintName("product_images_ibfk_1");
        });

        modelBuilder.Entity<products>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.brand_id, "brand_id");

            entity.HasIndex(e => e.category_id, "cateogry_id");

            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.description).HasMaxLength(255);
            entity.Property(e => e.product_name).HasMaxLength(255);
            entity.Property(e => e.updated_at).HasColumnType("timestamp");

            entity.HasOne(d => d.brand).WithMany(p => p.products)
                .HasForeignKey(d => d.brand_id)
                .HasConstraintName("products_ibfk_2");

            entity.HasOne(d => d.category).WithMany(p => p.products)
                .HasForeignKey(d => d.category_id)
                .HasConstraintName("products_ibfk_1");
        });

        modelBuilder.Entity<products_cart_items>(entity =>
        {
            entity.HasKey(e => new { e.products_id, e.cart_items_product_id }).HasName("PRIMARY");

            entity.HasOne(d => d.products).WithMany(p => p.products_cart_items)
                .HasForeignKey(d => d.products_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_cart_items_ibfk_1");
        });

        modelBuilder.Entity<user_profiles>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.address).HasMaxLength(255);
            entity.Property(e => e.avatar_url).HasMaxLength(255);
            entity.Property(e => e.perferences).HasMaxLength(255);
        });

        modelBuilder.Entity<users>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.created_at).HasColumnType("timestamp");
            entity.Property(e => e.email).HasMaxLength(32);
            entity.Property(e => e.password).HasMaxLength(32);
            entity.Property(e => e.updated_at).HasColumnType("timestamp");
            entity.Property(e => e.username).HasMaxLength(32);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
