namespace UP.Application.DTOs;

public class UsuarioDto
{
    public int UsuarioId { get; set; }
    public int? RolId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Rne { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public bool? EsActivo { get; set; }
}
