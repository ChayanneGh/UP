using System;

namespace UP.Application.DTOs;

public class SalidaDto
{
    public int SalidaId { get; set; }
    public DateTime? HoraSalida { get; set; }
    public int EntradaId { get; set; }
}
