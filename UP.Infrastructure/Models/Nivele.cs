using System;
using System.Collections.Generic;

namespace P4rquing.Infraestructure.Models;

public partial class Nivele
{
    public int NivelId { get; set; }

    public int? Capacidad { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Estacionamiento> Estacionamientos { get; set; } = new List<Estacionamiento>();
}
