using System;

namespace UP.Application.DTOs;

public class TarifaDto
{
    public int TarifaId { get; set; }
    public decimal TarifaHora { get; set; }
    public decimal TarifaDia { get; set; }
    public DateTime VigenciaInicio { get; set; }
    public bool? EsActiva { get; set; }
}
