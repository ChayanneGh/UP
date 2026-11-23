using System;
using System.Collections.Generic;

namespace UP.Infrastructure.Entities;

public partial class TiposVehiculo
{
    public int TipoVehiculoId { get; set; }

    public string Nombre { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore] public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
