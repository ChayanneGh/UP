namespace UP.Application.DTOs;

public class VehiculoDto
{
    public int VehiculoId { get; set; }
    public int PropietarioId { get; set; }
    public string Placa { get; set; } = string.Empty;
    public int TipoVehiculoId { get; set; }
}
