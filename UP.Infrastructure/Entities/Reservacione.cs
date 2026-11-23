using System;
using System.Collections.Generic;

namespace UP.Infrastructure.Entities;

public partial class Reservacione
{
    public int ReservacionId { get; set; }

    public int EspacioId { get; set; }

    public int UsuarioId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore] public virtual Estacionamiento Espacio { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore] public virtual Usuario Usuario { get; set; } = null!;
}
