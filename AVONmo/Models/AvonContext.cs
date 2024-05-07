using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AVONmo.Models;

public partial class AvonContext : DbContext
{
    public AvonContext()
    {
    }

    public AvonContext(DbContextOptions<AvonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Crema> Cremas { get; set; }

    public virtual DbSet<Electrodomestico> Electrodomesticos { get; set; }

    public virtual DbSet<Maquillaje> Maquillajes { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Perfume> Perfumes { get; set; }

    public virtual DbSet<Precio> Precios { get; set; }

    public virtual DbSet<Tupper> Tuppers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=avonmo-server.database.windows.net,1433;Database=avonmo-database;User=avonmo-server-admin;Password=03RFMD6NS6W167L5$");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__categori__CB90334970B864E4");

            entity.ToTable("categoria");

            entity.Property(e => e.IdCategoria)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Categoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(129)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(129)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Crema>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Cremas__2085A9CF8A12F4C2");

            entity.Property(e => e.IdProducto)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Producto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.IdCategoria)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Categoria");
            entity.Property(e => e.IdPrecio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Precio");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Cremas)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Cremas__Id_Categ__5070F446");

            entity.HasOne(d => d.IdPrecioNavigation).WithMany(p => p.Cremas)
                .HasForeignKey(d => d.IdPrecio)
                .HasConstraintName("FK__Cremas__Id_Preci__5165187F");
        });

        modelBuilder.Entity<Electrodomestico>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Electrod__2085A9CF9411FCE8");

            entity.Property(e => e.IdProducto)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Producto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.IdCategoria)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Categoria");
            entity.Property(e => e.IdPrecio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Precio");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Electrodomesticos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Electrodo__Id_Ca__5812160E");

            entity.HasOne(d => d.IdPrecioNavigation).WithMany(p => p.Electrodomesticos)
                .HasForeignKey(d => d.IdPrecio)
                .HasConstraintName("FK__Electrodo__Id_Pr__59063A47");
        });

        modelBuilder.Entity<Maquillaje>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Maquilla__2085A9CF6F588F6C");

            entity.ToTable("Maquillaje");

            entity.Property(e => e.IdProducto)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Producto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.IdCategoria)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Categoria");
            entity.Property(e => e.IdPrecio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Precio");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Maquillajes)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Maquillaj__Id_Ca__5BE2A6F2");

            entity.HasOne(d => d.IdPrecioNavigation).WithMany(p => p.Maquillajes)
                .HasForeignKey(d => d.IdPrecio)
                .HasConstraintName("FK__Maquillaj__Id_Pr__5CD6CB2B");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__F6BCBF2E6D11E1ED");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu)
                .ValueGeneratedNever()
                .HasColumnName("Id_Menu");
            entity.Property(e => e.CantInu).HasColumnName("cant_Inu");
            entity.Property(e => e.IdCategoria)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Categoria");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Menus)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Menu__Id_Categor__4BAC3F29");
        });

        modelBuilder.Entity<Perfume>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Perfumes__2085A9CFA07A0931");

            entity.Property(e => e.IdProducto)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Producto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.IdCategoria)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Categoria");
            entity.Property(e => e.IdPrecio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Precio");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Perfumes)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Perfumes__Id_Cat__5441852A");

            entity.HasOne(d => d.IdPrecioNavigation).WithMany(p => p.Perfumes)
                .HasForeignKey(d => d.IdPrecio)
                .HasConstraintName("FK__Perfumes__Id_Pre__5535A963");
        });

        modelBuilder.Entity<Precio>(entity =>
        {
            entity.HasKey(e => e.IdPrecio).HasName("PK__Precios__95137B042F1E692F");

            entity.Property(e => e.IdPrecio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Precio");
        });

        modelBuilder.Entity<Tupper>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Tuppers__2085A9CFC2256104");

            entity.Property(e => e.IdProducto)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Producto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.IdCategoria)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Categoria");
            entity.Property(e => e.IdPrecio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Precio");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Tuppers)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Tuppers__Id_Cate__5FB337D6");

            entity.HasOne(d => d.IdPrecioNavigation).WithMany(p => p.Tuppers)
                .HasForeignKey(d => d.IdPrecio)
                .HasConstraintName("FK__Tuppers__Id_Prec__60A75C0F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
