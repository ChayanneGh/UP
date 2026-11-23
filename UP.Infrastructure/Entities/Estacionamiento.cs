using System;
using System.Collections.Generic;

namespace UP.Infrastructure.Entities;

public partial class Estacionamiento
{
    public int EstacionamientoId { get; set; }

    public int NivelId { get; set; }

    public bool? Estado { get; set; }

    [System.Text.Json.Serialization.JsonIgnore] public virtual ICollection<Entrada> Entrada { get; set; } = new List<Entrada>();

    [System.Text.Json.Serialization.JsonIgnore] public virtual Nivele Nivel { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore] public virtual ICollection<Reservacione> Reservaciones { get; set; } = new List<Reservacione>();
}
