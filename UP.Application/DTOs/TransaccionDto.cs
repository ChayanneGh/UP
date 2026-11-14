using System;

namespace UP.Application.DTOs;

public class TransaccionDto
{
    public int TransaccionId { get; set; }
    public int TarifaId { get; set; }
    public int EntradaId { get; set; }
    public int SalidaId { get; set; }
    public int UsuarioId { get; set; }
    public decimal Monto { get; set; }
    public DateTime? Fecha { get; set; }
}
