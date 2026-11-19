using System;
using System.Collections.Generic;

namespace UP.Infrastructure.Entities;

public partial class Reservacione
{
    public int ReservacionId { get; set; }

    public int EstacionamientoId { get; set; }

    public int UsuarioId { get; set; }

    public virtual Estacionamiento Estacionamiento { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
