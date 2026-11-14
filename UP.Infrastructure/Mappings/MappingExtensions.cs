using UP.Application.DTOs;
using UP.Infrastructure.Models;

namespace UP.Infrastructure.Mappings;

public static class MappingExtensions
{
    // ==========================================
    // Entrada Mappings
    // ==========================================
    public static EntradaDto ToDto(this Entrada entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new EntradaDto
        {
            EntradaId = entity.EntradaId,
            HoraEntrada = entity.HoraEntrada,
            EspacioId = entity.EspacioId,
            UsuarioId = entity.UsuarioId,
            Placa = entity.Placa,
            Estado = entity.Estado
        };
    }

    public static Entrada ToEntity(this EntradaDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Entrada
        {
            EntradaId = dto.EntradaId,
            HoraEntrada = dto.HoraEntrada,
            EspacioId = dto.EspacioId,
            UsuarioId = dto.UsuarioId,
            Placa = dto.Placa,
            Estado = dto.Estado
        };
    }

    public static void UpdateEntity(this EntradaDto dto, Entrada entity)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(entity);

        entity.HoraEntrada = dto.HoraEntrada;
        entity.EspacioId = dto.EspacioId;
        entity.UsuarioId = dto.UsuarioId;
        entity.Placa = dto.Placa;
        entity.Estado = dto.Estado;
    }

    // ==========================================
    // Estacionamiento Mappings
    // ==========================================
    public static EstacionamientoDto ToDto(this Estacionamiento entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new EstacionamientoDto
        {
            EstacionamientoId = entity.EstacionamientoId,
            NivelId = entity.NivelId,
            Estado = entity.Estado
        };
    }

    public static Estacionamiento ToEntity(this EstacionamientoDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Estacionamiento
        {
            EstacionamientoId = dto.EstacionamientoId,
            NivelId = dto.NivelId,
            Estado = dto.Estado
        };
    }

    public static void UpdateEntity(this EstacionamientoDto dto, Estacionamiento entity)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(entity);

        entity.NivelId = dto.NivelId;
        entity.Estado = dto.Estado;
    }

    // ==========================================
    // Nivele / Nivel Mappings
    // ==========================================
    public static NivelDto ToDto(this Nivele entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new NivelDto
        {
            NivelId = entity.NivelId,
            Capacidad = entity.Capacidad,
            Nombre = entity.Nombre
        };
    }

    public static Nivele ToEntity(this NivelDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Nivele
        {
            NivelId = dto.NivelId,
            Capacidad = dto.Capacidad,
            Nombre = dto.Nombre
        };
    }

    public static void UpdateEntity(this NivelDto dto, Nivele entity)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(entity);

        entity.Capacidad = dto.Capacidad;
        entity.Nombre = dto.Nombre;
    }

    // ==========================================
    // Reservacione / Reservacion Mappings
    // ==========================================
    public static ReservacionDto ToDto(this Reservacione entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new ReservacionDto
        {
            ReservacionId = entity.ReservacionId,
            EspacioId = entity.EspacioId,
            UsuarioId = entity.UsuarioId
        };
    }

    public static Reservacione ToEntity(this ReservacionDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Reservacione
        {
            ReservacionId = dto.ReservacionId,
            EspacioId = dto.EspacioId,
            UsuarioId = dto.UsuarioId
        };
    }

    public static void UpdateEntity(this ReservacionDto dto, Reservacione entity)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(entity);

        entity.EspacioId = dto.EspacioId;
        entity.UsuarioId = dto.UsuarioId;
    }

    // ==========================================
    // Role / Rol Mappings
    // ==========================================
    public static RolDto ToDto(this Role entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new RolDto
        {
            RolId = entity.RolId,
            Nombre = entity.Nombre
        };
    }

    public static Role ToEntity(this RolDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Role
        {
            RolId = dto.RolId,
            Nombre = dto.Nombre
        };
    }

    public static void UpdateEntity(this RolDto dto, Role entity)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(entity);

        entity.Nombre = dto.Nombre;
    }

    // ==========================================
    // Salida Mappings
    // ==========================================
    public static SalidaDto ToDto(this Salida entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new SalidaDto
        {
            SalidaId = entity.SalidaId,
            HoraSalida = entity.HoraSalida,
            EntradaId = entity.EntradaId
        };
    }

    public static Salida ToEntity(this SalidaDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Salida
        {
            SalidaId = dto.SalidaId,
            HoraSalida = dto.HoraSalida,
            EntradaId = dto.EntradaId
        };
    }

    public static void UpdateEntity(this SalidaDto dto, Salida entity)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(entity);

        entity.HoraSalida = dto.HoraSalida;
        entity.EntradaId = dto.EntradaId;
    }

    // ==========================================
    // Tarifa Mappings
    // ==========================================
    public static TarifaDto ToDto(this Tarifa entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new TarifaDto
        {
            TarifaId = entity.TarifaId,
            TarifaHora = entity.TarifaHora,
            TarifaDia = entity.TarifaDia,
            VigenciaInicio = entity.VigenciaInicio,
            EsActiva = entity.EsActiva
        };
    }

    public static Tarifa ToEntity(this TarifaDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Tarifa
        {
            TarifaId = dto.TarifaId,
            TarifaHora = dto.TarifaHora,
            TarifaDia = dto.TarifaDia,
            VigenciaInicio = dto.VigenciaInicio,
            EsActiva = dto.EsActiva
        };
    }

    public static void UpdateEntity(this TarifaDto dto, Tarifa entity)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(entity);

        entity.TarifaHora = dto.TarifaHora;
        entity.TarifaDia = dto.TarifaDia;
        entity.VigenciaInicio = dto.VigenciaInicio;
        entity.EsActiva = dto.EsActiva;
    }

    // ==========================================
    // TiposVehiculo / TipoVehiculo Mappings
    // ==========================================
    public static TipoVehiculoDto ToDto(this TiposVehiculo entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new TipoVehiculoDto
        {
            TipoVehiculoId = entity.TipoVehiculoId,
            Nombre = entity.Nombre
        };
    }

    public static TiposVehiculo ToEntity(this TipoVehiculoDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new TiposVehiculo
        {
            TipoVehiculoId = dto.TipoVehiculoId,
            Nombre = dto.Nombre
        };
    }

    public static void UpdateEntity(this TipoVehiculoDto dto, TiposVehiculo entity)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(entity);

        entity.Nombre = dto.Nombre;
    }

    // ==========================================
    // Transaccione / Transaccion Mappings
    // ==========================================
    public static TransaccionDto ToDto(this Transaccione entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new TransaccionDto
        {
            TransaccionId = entity.TransaccionId,
            TarifaId = entity.TarifaId,
            EntradaId = entity.EntradaId,
            SalidaId = entity.SalidaId,
            UsuarioId = entity.UsuarioId,
            Monto = entity.Monto,
            Fecha = entity.Fecha
        };
    }

    public static Transaccione ToEntity(this TransaccionDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Transaccione
        {
            TransaccionId = dto.TransaccionId,
            TarifaId = dto.TarifaId,
            EntradaId = dto.EntradaId,
            SalidaId = dto.SalidaId,
            UsuarioId = dto.UsuarioId,
            Monto = dto.Monto,
            Fecha = dto.Fecha
        };
    }

    public static void UpdateEntity(this TransaccionDto dto, Transaccione entity)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(entity);

        entity.TarifaId = dto.TarifaId;
        entity.EntradaId = dto.EntradaId;
        entity.SalidaId = dto.SalidaId;
        entity.UsuarioId = dto.UsuarioId;
        entity.Monto = dto.Monto;
        entity.Fecha = dto.Fecha;
    }

    // ==========================================
    // Usuario Mappings
    // ==========================================
    public static UsuarioDto ToDto(this Usuario entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new UsuarioDto
        {
            UsuarioId = entity.UsuarioId,
            RolId = entity.RolId,
            Nombre = entity.Nombre,
            Rne = entity.Rne,
            Telefono = entity.Telefono,
            Correo = entity.Correo,
            EsActivo = entity.EsActivo
        };
    }

    public static Usuario ToEntity(this UsuarioDto dto, string contrasena = "")
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Usuario
        {
            UsuarioId = dto.UsuarioId,
            RolId = dto.RolId,
            Nombre = dto.Nombre,
            Rne = dto.Rne,
            Telefono = dto.Telefono,
            Correo = dto.Correo,
            Contrasena = contrasena,
            EsActivo = dto.EsActivo
        };
    }

    public static void UpdateEntity(this UsuarioDto dto, Usuario entity)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(entity);

        entity.RolId = dto.RolId;
        entity.Nombre = dto.Nombre;
        entity.Rne = dto.Rne;
        entity.Telefono = dto.Telefono;
        entity.Correo = dto.Correo;
        entity.EsActivo = dto.EsActivo;
    }

    // ==========================================
    // Vehiculo Mappings
    // ==========================================
    public static VehiculoDto ToDto(this Vehiculo entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new VehiculoDto
        {
            VehiculoId = entity.VehiculoId,
            PropietarioId = entity.PropietarioId,
            Placa = entity.Placa,
            TipoVehiculoId = entity.TipoVehiculoId
        };
    }

    public static Vehiculo ToEntity(this VehiculoDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Vehiculo
        {
            VehiculoId = dto.VehiculoId,
            PropietarioId = dto.PropietarioId,
            Placa = dto.Placa,
            TipoVehiculoId = dto.TipoVehiculoId
        };
    }

    public static void UpdateEntity(this VehiculoDto dto, Vehiculo entity)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(entity);

        entity.PropietarioId = dto.PropietarioId;
        entity.Placa = dto.Placa;
        entity.TipoVehiculoId = dto.TipoVehiculoId;
    }
}
