using System;

namespace UP.Application.DTOs;

public class EntradaDto
{
    public int EntradaId { get; set; }
    public DateTime? HoraEntrada { get; set; }
    public int EspacioId { get; set; }
    public int UsuarioId { get; set; }
    public string Placa { get; set; } = string.Empty;
    public bool? Estado { get; set; }
}
