using System;
using System.Collections.Generic;

namespace P4rquing.Infraestructure.Models;

public partial class Estacionamiento
{
    public int EstacionamientoId { get; set; }

    public int NivelId { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Entrada> Entrada { get; set; } = new List<Entrada>();

    public virtual Nivele Nivel { get; set; } = null!;

    public virtual ICollection<Reservacione> Reservaciones { get; set; } = new List<Reservacione>();
}
