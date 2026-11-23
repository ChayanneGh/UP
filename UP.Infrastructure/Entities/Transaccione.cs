using System;
using System.Collections.Generic;

namespace UP.Infrastructure.Entities;

public partial class Transaccione
{
    public int TransaccionId { get; set; }

    public int TarifaId { get; set; }

    public int EntradaId { get; set; }

    public int SalidaId { get; set; }

    public int UsuarioId { get; set; }

    public decimal Monto { get; set; }

    public DateTime? Fecha { get; set; }

    [System.Text.Json.Serialization.JsonIgnore] public virtual Entrada Entrada { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore] public virtual Salida Salida { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore] public virtual Tarifa Tarifa { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore] public virtual Usuario Usuario { get; set; } = null!;
}
