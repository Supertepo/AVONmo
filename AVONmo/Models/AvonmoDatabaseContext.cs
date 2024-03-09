using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AVONmo.Models;

public partial class AvonmoDatabaseContext : DbContext
{
    public AvonmoDatabaseContext()
    {
    }

    public AvonmoDatabaseContext(DbContextOptions<AvonmoDatabaseContext> options)
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=avonmo-server.database.windows.net,1433;Database=avonmo-database;User=avonmo-server-admin;Password=03RFMD6NS6W167L5$");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__categori__CB9033496ACEFBB9");

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
            entity.HasKey(e => e.IdProducto).HasName("PK__Cremas__2085A9CFFD2A6C68");

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
                .HasConstraintName("FK__Cremas__Id_Categ__6383C8BA");

            entity.HasOne(d => d.IdPrecioNavigation).WithMany(p => p.Cremas)
                .HasForeignKey(d => d.IdPrecio)
                .HasConstraintName("FK__Cremas__Id_Preci__6477ECF3");
        });

        modelBuilder.Entity<Electrodomestico>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Electrod__2085A9CF41B67A81");

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
                .HasConstraintName("FK__Electrodo__Id_Ca__6B24EA82");

            entity.HasOne(d => d.IdPrecioNavigation).WithMany(p => p.Electrodomesticos)
                .HasForeignKey(d => d.IdPrecio)
                .HasConstraintName("FK__Electrodo__Id_Pr__6C190EBB");
        });

        modelBuilder.Entity<Maquillaje>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Maquilla__2085A9CF331C8249");

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
                .HasConstraintName("FK__Maquillaj__Id_Ca__6EF57B66");

            entity.HasOne(d => d.IdPrecioNavigation).WithMany(p => p.Maquillajes)
                .HasForeignKey(d => d.IdPrecio)
                .HasConstraintName("FK__Maquillaj__Id_Pr__6FE99F9F");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__F6BCBF2E43BAC17B");

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
                .HasConstraintName("FK__Menu__Id_Categor__5EBF139D");
        });

        modelBuilder.Entity<Perfume>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Perfumes__2085A9CF27C56169");

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
                .HasConstraintName("FK__Perfumes__Id_Cat__6754599E");

            entity.HasOne(d => d.IdPrecioNavigation).WithMany(p => p.Perfumes)
                .HasForeignKey(d => d.IdPrecio)
                .HasConstraintName("FK__Perfumes__Id_Pre__68487DD7");
        });

        modelBuilder.Entity<Precio>(entity =>
        {
            entity.HasKey(e => e.IdPrecio).HasName("PK__Precios__95137B0409F00FA2");

            entity.Property(e => e.IdPrecio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Id_Precio");
        });

        modelBuilder.Entity<Tupper>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Tuppers__2085A9CF395FF4E3");

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
                .HasConstraintName("FK__Tuppers__Id_Cate__72C60C4A");

            entity.HasOne(d => d.IdPrecioNavigation).WithMany(p => p.Tuppers)
                .HasForeignKey(d => d.IdPrecio)
                .HasConstraintName("FK__Tuppers__Id_Prec__73BA3083");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
