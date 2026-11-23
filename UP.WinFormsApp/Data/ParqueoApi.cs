using System.Net;
using System.Net.Http.Json;

namespace UP.WinFormsApp.Data;

public sealed class UsuarioSesion
{
    public int UsuarioId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int RolId { get; set; }
    public string Rol { get; set; } = string.Empty;
}

public sealed class EspacioDisponible
{
    public int EstacionamientoId { get; set; }
    public int NivelId { get; set; }
    public string Nivel { get; set; } = string.Empty;
    public bool Reservado { get; set; }
}

public sealed class ReservacionItem
{
    public int ReservacionId { get; set; }
    public int EspacioId { get; set; }
    public int UsuarioId { get; set; }
    public string UsuarioNombre { get; set; } = string.Empty;
}

public sealed record ResultadoSalida(
    int EntradaId,
    string Placa,
    int EspacioId,
    DateTime HoraEntrada,
    DateTime HoraSalida,
    decimal Monto,
    int HorasCobradas);

/// <summary>
/// Cliente HTTP del API. El escritorio no conoce la base de datos:
/// toda la lógica (login, entradas, salidas, reservas) vive en /api/Caja.
/// </summary>
public static class ParqueoApi
{
    private static readonly HttpClient Cliente = new()
    {
        BaseAddress = new Uri(ApiConfig.BaseUrl),
        Timeout = TimeSpan.FromSeconds(30)
    };

    public static async Task<UsuarioSesion?> LoginAsync(string usuario, string contrasena)
    {
        using var respuesta = await Cliente.PostAsJsonAsync(
            "api/Caja/login", new { usuario, contrasena });

        if (respuesta.StatusCode == HttpStatusCode.Unauthorized)
            return null;

        await VerificarAsync(respuesta);
        return await respuesta.Content.ReadFromJsonAsync<UsuarioSesion>();
    }

    public static async Task<List<EspacioDisponible>> ObtenerDisponibilidadAsync()
    {
        using var respuesta = await Cliente.GetAsync("api/Caja/disponibilidad");
        await VerificarAsync(respuesta);

        return await respuesta.Content.ReadFromJsonAsync<List<EspacioDisponible>>() ?? [];
    }

    public static async Task<List<ReservacionItem>> ObtenerReservacionesAsync()
    {
        using var respuesta = await Cliente.GetAsync("api/Caja/reservaciones");
        await VerificarAsync(respuesta);

        return await respuesta.Content.ReadFromJsonAsync<List<ReservacionItem>>() ?? [];
    }

    public static async Task<int> RegistrarEntradaAsync(string placa, int espacioId)
    {
        using var respuesta = await Cliente.PostAsJsonAsync(
            "api/Caja/entradas", new { placa, espacioId });

        await VerificarAsync(respuesta);

        var entrada = await respuesta.Content.ReadFromJsonAsync<EntradaCreada>();
        return entrada?.EntradaId ?? 0;
    }

    public static async Task<ResultadoSalida> RegistrarSalidaAsync(string placa)
    {
        using var respuesta = await Cliente.PostAsJsonAsync(
            "api/Caja/salidas", new { placa });

        await VerificarAsync(respuesta);

        return await respuesta.Content.ReadFromJsonAsync<ResultadoSalida>()
               ?? throw new InvalidOperationException("La API no devolvió el resultado de la salida.");
    }

    public static async Task<int> ConfirmarReservaAsync(int reservacionId)
    {
        using var respuesta = await Cliente.PostAsync(
            $"api/Caja/reservaciones/{reservacionId}/confirmar", content: null);

        await VerificarAsync(respuesta);

        var entrada = await respuesta.Content.ReadFromJsonAsync<EntradaCreada>();
        return entrada?.EntradaId ?? 0;
    }

    private static async Task VerificarAsync(HttpResponseMessage respuesta)
    {
        if (respuesta.IsSuccessStatusCode)
            return;

        throw new InvalidOperationException(await LeerMensajeAsync(respuesta));
    }

    private static async Task<string> LeerMensajeAsync(HttpResponseMessage respuesta)
    {
        try
        {
            var error = await respuesta.Content.ReadFromJsonAsync<ErrorApi>();
            if (!string.IsNullOrWhiteSpace(error?.Mensaje))
                return error!.Mensaje;
        }
        catch
        {
            // Si el cuerpo no es el JSON esperado se usa el mensaje genérico.
        }

        return $"La API respondió {(int)respuesta.StatusCode} ({respuesta.ReasonPhrase}).";
    }

    private sealed record EntradaCreada(int EntradaId);

    private sealed record ErrorApi(string? Mensaje);
}
