using System;
using System.Collections.Generic;

namespace UP.Infrastructure.Models;

public partial class Salida
{
    public int SalidaId { get; set; }

    public DateTime? HoraSalida { get; set; }

    public int EntradaId { get; set; }

    public virtual Entrada Entrada { get; set; } = null!;

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
