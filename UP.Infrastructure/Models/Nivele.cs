using System;
using System.Collections.Generic;

namespace UP.Infrastructure.Models;

public partial class Nivele
{
    public int NivelId { get; set; }

    public int? Capacidad { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Estacionamiento> Estacionamientos { get; set; } = new List<Estacionamiento>();
}
