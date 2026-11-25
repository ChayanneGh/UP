using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Api.Controllers;

/// <summary>
/// Operaciones de caja/operador que usa la aplicación de escritorio.
/// Toda la lógica y las transacciones viven en el API; el cliente solo consume HTTP.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CajaController(IDbContextFactory<UPContext> contextFactory) : ControllerBase
{
    // ---------- Contratos ----------

    public record LoginRequest(string Usuario, string Contrasena);

    public record LoginResponse(int UsuarioId, string Nombre, int RolId, string Rol);

    public record EspacioDisponibleResponse(int EstacionamientoId, int NivelId, string Nivel, bool Reservado);

    public record ReservacionResponse(int ReservacionId, int EspacioId, int UsuarioId, string UsuarioNombre);

    public record EntradaRequest(string Placa, int EspacioId);

    public record EntradaResponse(int EntradaId, string Placa, int EspacioId);

    public record SalidaRequest(string Placa);

    public record SalidaResponse(
        int EntradaId,
        string Placa,
        int EspacioId,
        DateTime HoraEntrada,
        DateTime HoraSalida,
        decimal Monto,
        int HorasCobradas);

    public record ConfirmarReservaResponse(int EntradaId);

    // ---------- Endpoints ----------

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var usuario = (request.Usuario ?? string.Empty).Trim();
        var contrasena = request.Contrasena ?? string.Empty;

        if (usuario.Length == 0 || contrasena.Length == 0)
            return BadRequest(new { mensaje = "Escriba el usuario y la contraseña." });

        await using var db = await contextFactory.CreateDbContextAsync();

        var encontrado = await db.Usuarios
            .AsNoTracking()
            .Where(u => (u.EsActivo ?? true)
                        && u.Contrasena == contrasena
                        && (u.Correo == usuario || u.Rne == usuario || u.Nombre == usuario))
            .Select(u => new { u.UsuarioId, u.Nombre, u.RolId })
            .FirstOrDefaultAsync();

        if (encontrado is null)
            return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos, o el usuario está inactivo." });

        var rolId = encontrado.RolId ?? 0;
        var rol = await db.Roles
            .AsNoTracking()
            .Where(r => r.RolId == rolId)
            .Select(r => r.Nombre)
            .FirstOrDefaultAsync() ?? string.Empty;

        return Ok(new LoginResponse(encontrado.UsuarioId, encontrado.Nombre, rolId, rol));
    }

    [HttpGet("disponibilidad")]
    public async Task<IActionResult> Disponibilidad()
    {
        await using var db = await contextFactory.CreateDbContextAsync();

        var ocupados = db.Entradas
            .Where(e => (e.Estado ?? false))
            .Select(e => e.EspacioId);

        var reservados = db.Reservaciones.Select(r => r.EspacioId);

        var lista = await db.Estacionamientos
            .AsNoTracking()
            .Where(e => (e.Estado ?? true) && !ocupados.Contains(e.EstacionamientoId))
            .OrderBy(e => e.NivelId)
            .ThenBy(e => e.EstacionamientoId)
            .Select(e => new EspacioDisponibleResponse(
                e.EstacionamientoId,
                e.NivelId,
                db.Niveles
                    .Where(n => n.NivelId == e.NivelId)
                    .Select(n => n.Nombre)
                    .FirstOrDefault() ?? string.Empty,
                reservados.Contains(e.EstacionamientoId)))
            .ToListAsync();

        return Ok(lista);
    }

    [HttpGet("reservaciones")]
    public async Task<IActionResult> Reservaciones()
    {
        await using var db = await contextFactory.CreateDbContextAsync();

        var lista = await db.Reservaciones
            .AsNoTracking()
            .OrderBy(r => r.ReservacionId)
            .Select(r => new ReservacionResponse(
                r.ReservacionId,
                r.EspacioId,
                r.UsuarioId,
                db.Usuarios
                    .Where(u => u.UsuarioId == r.UsuarioId)
                    .Select(u => u.Nombre)
                    .FirstOrDefault() ?? string.Empty))
            .ToListAsync();

        return Ok(lista);
    }

    [HttpPost("entradas")]
    public async Task<IActionResult> RegistrarEntrada([FromBody] EntradaRequest request)
    {
        var placa = (request.Placa ?? string.Empty).Trim().ToUpperInvariant();

        if (placa.Length == 0)
            return BadRequest(new { mensaje = "Escriba la placa del vehículo." });

        await using var db = await contextFactory.CreateDbContextAsync();
        await using var transaccion = await db.Database.BeginTransactionAsync();

        var vehiculo = await db.Vehiculos
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Placa == placa);

        if (vehiculo is null)
            return BadRequest(new { mensaje = $"La placa '{placa}' no está registrada en el sistema." });

        var espacioExiste = await db.Estacionamientos
            .AnyAsync(e => e.EstacionamientoId == request.EspacioId && (e.Estado ?? true));

        if (!espacioExiste)
            return BadRequest(new { mensaje = $"El espacio {request.EspacioId} no existe o está deshabilitado." });

        var ocupado = await db.Entradas
            .AnyAsync(e => e.EspacioId == request.EspacioId && (e.Estado ?? false));

        if (ocupado)
            return BadRequest(new { mensaje = $"El espacio {request.EspacioId} ya está ocupado." });

        var entradaActiva = await db.Entradas
            .AnyAsync(e => e.Placa == placa && (e.Estado ?? false));

        if (entradaActiva)
            return BadRequest(new { mensaje = $"La placa {placa} ya tiene una entrada activa (aún no ha salido)." });

        var entrada = new Entrada
        {
            HoraEntrada = DateTime.Now,
            EspacioId = request.EspacioId,
            UsuarioId = vehiculo.PropietarioId,
            Placa = placa,
            Estado = true
        };

        db.Entradas.Add(entrada);
        await db.SaveChangesAsync();
        await transaccion.CommitAsync();

        return Ok(new EntradaResponse(entrada.EntradaId, placa, request.EspacioId));
    }

    [HttpPost("salidas")]
    public async Task<IActionResult> RegistrarSalida([FromBody] SalidaRequest request)
    {
        var placa = (request.Placa ?? string.Empty).Trim().ToUpperInvariant();

        if (placa.Length == 0)
            return BadRequest(new { mensaje = "Escriba la placa del vehículo." });

        await using var db = await contextFactory.CreateDbContextAsync();
        await using var transaccion = await db.Database.BeginTransactionAsync();

        var entrada = await db.Entradas
            .FirstOrDefaultAsync(e => e.Placa == placa && (e.Estado ?? false));

        if (entrada is null)
            return BadRequest(new { mensaje = $"No hay una entrada activa para la placa {placa}." });

        var tarifa = await db.Tarifas
            .AsNoTracking()
            .Where(t => (t.EsActiva ?? false))
            .OrderByDescending(t => t.VigenciaInicio)
            .FirstOrDefaultAsync();

        if (tarifa is null)
            return BadRequest(new { mensaje = "No hay una tarifa activa configurada." });

        var horaEntrada = entrada.HoraEntrada ?? DateTime.Now;
        var horaSalida = DateTime.Now;
        var horas = Math.Max(0, (horaSalida - horaEntrada).TotalHours);
        var horasCobradas = Math.Max(1, (int)Math.Ceiling(horas));

        decimal monto;
        if (horas >= 24 && tarifa.TarifaDia > 0)
        {
            var dias = (int)Math.Ceiling(horas / 24);
            monto = dias * tarifa.TarifaDia;
        }
        else
        {
            monto = horasCobradas * tarifa.TarifaHora;
        }

        var salida = new Salida
        {
            HoraSalida = horaSalida,
            EntradaId = entrada.EntradaId
        };

        db.Salidas.Add(salida);
        await db.SaveChangesAsync();

        db.Transacciones.Add(new Transaccione
        {
            TarifaId = tarifa.TarifaId,
            EntradaId = entrada.EntradaId,
            SalidaId = salida.SalidaId,
            UsuarioId = entrada.UsuarioId,
            Monto = monto,
            Fecha = horaSalida
        });

        entrada.Estado = false;
        await db.SaveChangesAsync();
        await transaccion.CommitAsync();

        return Ok(new SalidaResponse(
            entrada.EntradaId,
            entrada.Placa,
            entrada.EspacioId,
            horaEntrada,
            horaSalida,
            monto,
            horasCobradas));
    }

    [HttpPost("reservaciones/{reservacionId:int}/confirmar")]
    public async Task<IActionResult> ConfirmarReserva(int reservacionId)
    {
        await using var db = await contextFactory.CreateDbContextAsync();
        await using var transaccion = await db.Database.BeginTransactionAsync();

        var reserva = await db.Reservaciones
            .FirstOrDefaultAsync(r => r.ReservacionId == reservacionId);

        if (reserva is null)
            return BadRequest(new { mensaje = "La reservación ya no existe." });

        var vehiculo = await db.Vehiculos
            .AsNoTracking()
            .Where(v => v.PropietarioId == reserva.UsuarioId)
            .OrderBy(v => v.VehiculoId)
            .FirstOrDefaultAsync();

        if (vehiculo is null)
            return BadRequest(new { mensaje = "El usuario de la reservación no tiene vehículos registrados." });

        var espacioExiste = await db.Estacionamientos
            .AnyAsync(e => e.EstacionamientoId == reserva.EspacioId && (e.Estado ?? true));

        if (!espacioExiste)
            return BadRequest(new { mensaje = $"El espacio {reserva.EspacioId} no está disponible." });

        var ocupado = await db.Entradas
            .AnyAsync(e => e.EspacioId == reserva.EspacioId && (e.Estado ?? false));

        if (ocupado)
            return BadRequest(new { mensaje = $"El espacio {reserva.EspacioId} está ocupado." });

        var entradaActiva = await db.Entradas
            .AnyAsync(e => e.Placa == vehiculo.Placa && (e.Estado ?? false));

        if (entradaActiva)
            return BadRequest(new { mensaje = $"El vehículo {vehiculo.Placa} ya tiene una entrada activa." });

        var entrada = new Entrada
        {
            HoraEntrada = DateTime.Now,
            EspacioId = reserva.EspacioId,
            UsuarioId = reserva.UsuarioId,
            Placa = vehiculo.Placa,
            Estado = true
        };

        db.Entradas.Add(entrada);
        db.Reservaciones.Remove(reserva);
        await db.SaveChangesAsync();
        await transaccion.CommitAsync();

        return Ok(new ConfirmarReservaResponse(entrada.EntradaId));
    }
}
