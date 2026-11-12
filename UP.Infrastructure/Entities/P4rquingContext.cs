using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UP.Infrastructure.Entities;

public partial class P4rquingContext : DbContext
{
    public P4rquingContext()
    {
    }

    public P4rquingContext(DbContextOptions<P4rquingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Entrada> Entradas { get; set; }

    public virtual DbSet<Estacionamiento> Estacionamientos { get; set; }

    public virtual DbSet<Nivele> Niveles { get; set; }

    public virtual DbSet<Reservacione> Reservaciones { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Salida> Salidas { get; set; }

    public virtual DbSet<Tarifa> Tarifas { get; set; }

    public virtual DbSet<TiposVehiculo> TiposVehiculos { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=AzureConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entrada>(entity =>
        {
            entity.HasKey(e => e.EntradaId).HasName("PK__Entradas__A0846674D59F1BDC");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.HoraEntrada)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Estacionamiento).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.EstacionamientoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entradas__Espaci__787EE5A0");

            entity.HasOne(d => d.PlacaNavigation).WithMany(p => p.Entrada)
                .HasPrincipalKey(p => p.Placa)
                .HasForeignKey(d => d.Placa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entradas__Placa__7A672E12");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entradas__Usuari__797309D9");
        });

        modelBuilder.Entity<Estacionamiento>(entity =>
        {
            entity.HasKey(e => e.EstacionamientoId).HasName("PK__Estacion__83CC1191672499DB");

            entity.Property(e => e.Estado).HasDefaultValue(true);

            entity.HasOne(d => d.Nivel).WithMany(p => p.Estacionamientos)
                .HasForeignKey(d => d.NivelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estaciona__Nivel__6FE99F9F");
        });

        modelBuilder.Entity<Nivele>(entity =>
        {
            entity.HasKey(e => e.NivelId).HasName("PK__Niveles__316FA277526948CD");

            entity.Property(e => e.Capacidad).HasDefaultValue(5);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reservacione>(entity =>
        {
            entity.HasKey(e => e.ReservacionId).HasName("PK__Reservac__145B3ED5CBB4E4FF");

            entity.HasOne(d => d.Estacionamiento).WithMany(p => p.Reservaciones)
                .HasForeignKey(d => d.EstacionamientoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__Espac__72C60C4A");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reservaciones)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__Usuar__73BA3083");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__F92302F1C8178567");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Salida>(entity =>
        {
            entity.HasKey(e => e.SalidaId).HasName("PK__Salidas__DC997163D5802994");

            entity.Property(e => e.HoraSalida)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Entrada).WithMany(p => p.Salida)
                .HasForeignKey(d => d.EntradaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Salidas__Entrada__7E37BEF6");
        });

        modelBuilder.Entity<Tarifa>(entity =>
        {
            entity.HasKey(e => e.TarifaId).HasName("PK__Tarifas__E81AC2FB82AD6DF6");

            entity.Property(e => e.EsActiva).HasDefaultValue(true);
            entity.Property(e => e.TarifaDia).HasColumnType("money");
            entity.Property(e => e.TarifaHora).HasColumnType("money");
            entity.Property(e => e.VigenciaInicio).HasColumnType("datetime");
        });

        modelBuilder.Entity<TiposVehiculo>(entity =>
        {
            entity.HasKey(e => e.TipoVehiculoId).HasName("PK__TiposVeh__1EA21D0D9441A659");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.TransaccionId).HasName("PK__Transacc__86A849FE4D17918F");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("money");

            entity.HasOne(d => d.Entrada).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.EntradaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Entra__05D8E0BE");

            entity.HasOne(d => d.Salida).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.SalidaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Salid__06CD04F7");

            entity.HasOne(d => d.Tarifa).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.TarifaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Tarif__04E4BC85");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Usuar__07C12930");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B8BA4F3BA7");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A19DAD1250F").IsUnique();

            entity.HasIndex(e => e.Rne, "UQ__Usuarios__CAF07DAE76AFD521").IsUnique();

            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EsActivo).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rne)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RolId).HasDefaultValue(1);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Usuarios__RolId__628FA481");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.VehiculoId).HasName("PK__Vehiculo__AA0886001BDAAF3C");

            entity.HasIndex(e => e.Placa, "UQ__Vehiculo__8310F99D7303E613").IsUnique();

            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Propietario).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.PropietarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehiculos__Propi__68487DD7");

            entity.HasOne(d => d.TipoVehiculo).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.TipoVehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehiculos__TipoV__693CA210");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
