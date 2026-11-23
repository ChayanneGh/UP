using System.Text.Json;

namespace UP.WinFormsApp.Data;

/// <summary>
/// Dirección base del API, configurable en appsettings.json ("ApiBaseUrl").
/// </summary>
internal static class ApiConfig
{
    private const string UrlPorDefecto = "http://localhost:5029/";

    public static string BaseUrl { get; } = Cargar();

    private static string Cargar()
    {
        try
        {
            var ruta = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (!File.Exists(ruta))
                return UrlPorDefecto;

            using var documento = JsonDocument.Parse(File.ReadAllText(ruta));

            if (documento.RootElement.TryGetProperty("ApiBaseUrl", out var valor) &&
                valor.GetString() is { Length: > 0 } url)
            {
                return url.EndsWith('/') ? url : url + "/";
            }
        }
        catch
        {
            // Si el JSON está mal formado se usa la URL por defecto.
        }

        return UrlPorDefecto;
    }
}
