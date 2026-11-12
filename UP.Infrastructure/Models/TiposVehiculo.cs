using System;
using System.Collections.Generic;

namespace P4rquing.Infraestructure.Models;

public partial class TiposVehiculo
{
    public int TipoVehiculoId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
