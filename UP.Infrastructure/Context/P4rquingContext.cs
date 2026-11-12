using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using P4rquing.Infraestructure.Models;

namespace P4rquing.Infraestructure.Context;

public partial class P4rquingContext : DbContext
{
    public P4rquingContext()
    {
        Entradas = null!;
        Estacionamientos = null!;
        Niveles = null!;
        Reservaciones = null!;
        Roles = null!;
        Salidas = null!;
        Tarifas = null!;
        TiposVehiculos = null!;
        Transacciones = null!;
        Usuarios = null!;
        Vehiculos = null!;
    }

    public P4rquingContext(DbContextOptions<P4rquingContext> options)
        : base(options)
    {
        Entradas = null!;
        Estacionamientos = null!;
        Niveles = null!;
        Reservaciones = null!;
        Roles = null!;
        Salidas = null!;
        Tarifas = null!;
        TiposVehiculos = null!;
        Transacciones = null!;
        Usuarios = null!;
        Vehiculos = null!;
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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entrada>(entity =>
        {
            entity.HasKey(e => e.EntradaId).HasName("PK__Entradas__A0846674BE5DFCBA");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.HoraEntrada)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Espacio).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.EspacioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entradas__Espaci__52593CB8");

            entity.HasOne(d => d.PlacaNavigation).WithMany(p => p.Entrada)
                .HasPrincipalKey(p => p.Placa)
                .HasForeignKey(d => d.Placa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entradas__Placa__5441852A");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entradas__Usuari__534D60F1");
        });

        modelBuilder.Entity<Estacionamiento>(entity =>
        {
            entity.HasKey(e => e.EstacionamientoId).HasName("PK__Estacion__83CC11918B5D37BE");

            entity.Property(e => e.Estado).HasDefaultValue(true);

            entity.HasOne(d => d.Nivel).WithMany(p => p.Estacionamientos)
                .HasForeignKey(d => d.NivelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estaciona__Nivel__49C3F6B7");
        });

        modelBuilder.Entity<Nivele>(entity =>
        {
            entity.HasKey(e => e.NivelId).HasName("PK__Niveles__316FA27731977961");

            entity.Property(e => e.Capacidad).HasDefaultValue(5);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reservacione>(entity =>
        {
            entity.HasKey(e => e.ReservacionId).HasName("PK__Reservac__145B3ED557E20E79");

            entity.HasOne(d => d.Espacio).WithMany(p => p.Reservaciones)
                .HasForeignKey(d => d.EspacioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__Espac__4CA06362");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reservaciones)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__Usuar__4D94879B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__F92302F183FF7DE5");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Salida>(entity =>
        {
            entity.HasKey(e => e.SalidaId).HasName("PK__Salidas__DC9971637AC99003");

            entity.Property(e => e.HoraSalida)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Entrada).WithMany(p => p.Salida)
                .HasForeignKey(d => d.EntradaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Salidas__Entrada__5812160E");
        });

        modelBuilder.Entity<Tarifa>(entity =>
        {
            entity.HasKey(e => e.TarifaId).HasName("PK__Tarifas__E81AC2FBE266060D");

            entity.Property(e => e.EsActiva).HasDefaultValue(true);
            entity.Property(e => e.TarifaDia).HasColumnType("money");
            entity.Property(e => e.TarifaHora).HasColumnType("money");
            entity.Property(e => e.VigenciaInicio).HasColumnType("datetime");
        });

        modelBuilder.Entity<TiposVehiculo>(entity =>
        {
            entity.HasKey(e => e.TipoVehiculoId).HasName("PK__TiposVeh__1EA21D0DD0FE3AA1");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.TransaccionId).HasName("PK__Transacc__86A849FEE1C1AB74");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("money");

            entity.HasOne(d => d.Entrada).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.EntradaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Entra__5FB337D6");

            entity.HasOne(d => d.Salida).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.SalidaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Salid__60A75C0F");

            entity.HasOne(d => d.Tarifa).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.TarifaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Tarif__5EBF139D");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Usuar__619B8048");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B857E749AF");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A197A537774").IsUnique();

            entity.HasIndex(e => e.Rne, "UQ__Usuarios__CAF07DAE309E7D52").IsUnique();

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
                .HasConstraintName("FK__Usuarios__RolId__3C69FB99");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.VehiculoId).HasName("PK__Vehiculo__AA0886008E292917");

            entity.HasIndex(e => e.Placa, "UQ__Vehiculo__8310F99D92B99EF6").IsUnique();

            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Propietario).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.PropietarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehiculos__Propi__4222D4EF");

            entity.HasOne(d => d.TipoVehiculo).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.TipoVehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehiculos__TipoV__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
