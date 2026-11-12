using System;
using System.Collections.Generic;

namespace P4rquing.Infraestructure.Models;

public partial class Reservacione
{
    public int ReservacionId { get; set; }

    public int EspacioId { get; set; }

    public int UsuarioId { get; set; }

    public virtual Estacionamiento Espacio { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
