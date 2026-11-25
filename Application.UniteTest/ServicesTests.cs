using Microsoft.EntityFrameworkCore;
using UP.Application.Service;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;
using Xunit;

namespace Application.UnitTests;

public sealed class TestDbContextFactory : IDbContextFactory<P4rquingContext>
{
    private readonly DbContextOptions<P4rquingContext> options;

    public TestDbContextFactory()
    {
        options = new DbContextOptionsBuilder<P4rquingContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    public P4rquingContext CreateDbContext() => new(options);

    public Task<P4rquingContext> CreateDbContextAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(CreateDbContext());
}

public class ServicesTests
{
    private static int NewId() => Random.Shared.Next(1, int.MaxValue);

    private static string NewPlate(string prefix)
    {
        var value = $"{prefix}{NewId()}";
        return value[..Math.Min(10, value.Length)];
    }

    [Fact]
    public async Task EntradaSrv_HandlesExistingAndMissingRecords()
    {
        var factory = new TestDbContextFactory();
        var service = new EntradaSrv(factory);
        var entity = new Entrada { EntradaId = NewId(), EstacionamientoId = NewId(), UsuarioId = NewId(), Placa = NewPlate("P") };

        Assert.True(await service.Create(entity));
        Assert.NotNull(await service.ReadOne(entity.EntradaId));
        Assert.Null(await service.ReadOne(NewId()));
        entity.Estado = false;
        Assert.True(await service.Update(entity.EntradaId, entity));
        Assert.False(await service.Update(NewId(), entity));
        Assert.True(await service.Delete(entity.EntradaId));
        Assert.False(await service.Delete(NewId()));
    }

    [Fact]
    public async Task SalidaSrv_HandlesExistingAndMissingRecords()
    {
        var factory = new TestDbContextFactory();
        var service = new SalidaSrv(factory);
        var entity = new Salida { SalidaId = NewId(), EntradaId = NewId() };

        Assert.True(await service.Create(entity));
        Assert.NotNull(await service.ReadOne(entity.SalidaId));
        Assert.Null(await service.ReadOne(NewId()));
        Assert.True(await service.Update(entity.SalidaId, entity));
        Assert.False(await service.Update(NewId(), entity));
        Assert.True(await service.Delete(entity.SalidaId));
        Assert.False(await service.Delete(NewId()));
    }

    [Fact]
    public async Task EstacionamientoSrv_HandlesExistingAndMissingRecords()
    {
        var factory = new TestDbContextFactory();
        var service = new EstacionamientoSrv(factory);
        var entity = new Estacionamiento { EstacionamientoId = NewId(), NivelId = NewId(), Estado = true };

        Assert.True(await service.Create(entity));
        Assert.NotNull(await service.ReadOne(entity.EstacionamientoId));
        Assert.Null(await service.ReadOne(NewId()));
        entity.Estado = false;
        Assert.True(await service.Update(entity.EstacionamientoId, entity));
        Assert.False(await service.Update(NewId(), entity));
        Assert.True(await service.Delete(entity.EstacionamientoId));
        Assert.False(await service.Delete(NewId()));
    }

    [Fact]
    public async Task NiveleSrv_HandlesExistingAndMissingRecords()
    {
        var factory = new TestDbContextFactory();
        var service = new NiveleSrv(factory);
        var entity = new Nivele { NivelId = NewId(), Capacidad = Random.Shared.Next(1, 100), Nombre = $"Nivel-{NewId()}" };

        Assert.True(await service.Create(entity));
        Assert.NotNull(await service.ReadOne(entity.NivelId));
        Assert.Null(await service.ReadOne(NewId()));
        entity.Capacidad = Random.Shared.Next(1, 100);
        Assert.True(await service.Update(entity.NivelId, entity));
        Assert.False(await service.Update(NewId(), entity));
        Assert.True(await service.Delete(entity.NivelId));
        Assert.False(await service.Delete(NewId()));
    }

    [Fact]
    public async Task ReservacioneSrv_HandlesExistingAndMissingRecords()
    {
        var factory = new TestDbContextFactory();
        var service = new ReservacioneSrv(factory);
        var entity = new Reservacione { ReservacionId = NewId(), EstacionamientoId = NewId(), UsuarioId = NewId() };

        Assert.True(await service.Create(entity));
        Assert.NotNull(await service.ReadOne(entity.ReservacionId));
        Assert.Null(await service.ReadOne(NewId()));
        Assert.True(await service.Update(entity.ReservacionId, entity));
        Assert.False(await service.Update(NewId(), entity));
        Assert.True(await service.Delete(entity.ReservacionId));
        Assert.False(await service.Delete(NewId()));
    }

    [Fact]
    public async Task RoleSrv_HandlesExistingAndMissingRecords()
    {
        var factory = new TestDbContextFactory();
        var service = new RoleSrv(factory);
        var entity = new Role { RolId = NewId(), Nombre = $"Role-{NewId()}" };

        Assert.True(await service.Create(entity));
        Assert.NotNull(await service.ReadOne(entity.RolId));
        Assert.Null(await service.ReadOne(NewId()));
        entity.Nombre = $"Updated-{NewId()}";
        Assert.True(await service.Update(entity.RolId, entity));
        Assert.False(await service.Update(NewId(), entity));
        Assert.True(await service.Delete(entity.RolId));
        Assert.False(await service.Delete(NewId()));
    }

    [Fact]
    public async Task TarifaSrv_HandlesExistingAndMissingRecords()
    {
        var factory = new TestDbContextFactory();
        var service = new TarifaSrv(factory);
        var entity = new Tarifa
        {
            TarifaId = NewId(),
            TarifaHora = Random.Shared.Next(1, 100),
            TarifaDia = Random.Shared.Next(100, 500),
            VigenciaInicio = DateTime.UtcNow,
            EsActiva = true
        };

        Assert.True(await service.Create(entity));
        Assert.NotNull(await service.ReadOne(entity.TarifaId));
        Assert.Null(await service.ReadOne(NewId()));
        entity.TarifaHora = Random.Shared.Next(1, 100);
        Assert.True(await service.Update(entity.TarifaId, entity));
        Assert.False(await service.Update(NewId(), entity));
        Assert.True(await service.Delete(entity.TarifaId));
        Assert.False(await service.Delete(NewId()));
    }

    [Fact]
    public async Task TiposVehiculoSrv_HandlesExistingAndMissingRecords()
    {
        var factory = new TestDbContextFactory();
        var service = new TiposVehiculoSrv(factory);
        var entity = new TiposVehiculo { TipoVehiculoId = NewId(), Nombre = $"Tipo-{NewId()}" };

        Assert.True(await service.Create(entity));
        Assert.NotNull(await service.ReadOne(entity.TipoVehiculoId));
        Assert.Null(await service.ReadOne(NewId()));
        entity.Nombre = $"Updated-{NewId()}";
        Assert.True(await service.Update(entity.TipoVehiculoId, entity));
        Assert.False(await service.Update(NewId(), entity));
        Assert.True(await service.Delete(entity.TipoVehiculoId));
        Assert.False(await service.Delete(NewId()));
    }

    [Fact]
    public async Task TransaccioneSrv_HandlesExistingAndMissingRecords()
    {
        var factory = new TestDbContextFactory();
        var service = new TransaccioneSrv(factory);
        var entity = new Transaccione
        {
            TransaccionId = NewId(),
            TarifaId = NewId(),
            EntradaId = NewId(),
            SalidaId = NewId(),
            UsuarioId = NewId(),
            Monto = Random.Shared.Next(1, 1000),
            Fecha = DateTime.UtcNow
        };

        Assert.True(await service.Create(entity));
        Assert.NotNull(await service.ReadOne(entity.TransaccionId));
        Assert.Null(await service.ReadOne(NewId()));
        entity.Monto = Random.Shared.Next(1, 1000);
        Assert.True(await service.Update(entity.TransaccionId, entity));
        Assert.False(await service.Update(NewId(), entity));
        Assert.True(await service.Delete(entity.TransaccionId));
        Assert.False(await service.Delete(NewId()));
    }

    [Fact]
    public async Task UsuarioSrv_HandlesExistingAndMissingRecords()
    {
        var factory = new TestDbContextFactory();
        var service = new UsuarioSrv(factory);
        var entity = new Usuario
        {
            UsuarioId = NewId(),
            RolId = NewId(),
            Nombre = $"User-{NewId()}",
            Rne = $"RNE-{NewId()}",
            Telefono = $"809{Random.Shared.Next(1000000, 9999999)}",
            Correo = $"user{NewId()}@test.local",
            Contrasena = $"Password-{NewId()}",
            EsActivo = true
        };

        Assert.True(await service.Create(entity));
        Assert.NotNull(await service.ReadOne(entity.UsuarioId));
        Assert.Null(await service.ReadOne(NewId()));
        entity.EsActivo = false;
        Assert.True(await service.Update(entity.UsuarioId, entity));
        Assert.False(await service.Update(NewId(), entity));
        Assert.True(await service.Delete(entity.UsuarioId));
        Assert.False(await service.Delete(NewId()));
    }

    [Fact]
    public async Task VehiculoSrv_HandlesExistingAndMissingRecords()
    {
        var factory = new TestDbContextFactory();
        var service = new VehiculoSrv(factory);
        var entity = new Vehiculo { VehiculoId = NewId(), PropietarioId = NewId(), Placa = NewPlate("V"), TipoVehiculoId = NewId() };

        Assert.True(await service.Create(entity));
        Assert.NotNull(await service.ReadOne(entity.VehiculoId));
        Assert.Null(await service.ReadOne(NewId()));
        Assert.True(await service.Update(entity.VehiculoId, entity));
        Assert.False(await service.Update(NewId(), entity));
        Assert.True(await service.Delete(entity.VehiculoId));
        Assert.False(await service.Delete(NewId()));
    }
}
