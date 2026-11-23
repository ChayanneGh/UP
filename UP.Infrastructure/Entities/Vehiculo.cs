using System;
using System.Collections.Generic;

namespace UP.Infrastructure.Entities;

public partial class Vehiculo
{
    public int VehiculoId { get; set; }

    public int PropietarioId { get; set; }

    public string Placa { get; set; } = null!;

    public int TipoVehiculoId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore] public virtual ICollection<Entrada> Entrada { get; set; } = new List<Entrada>();

    [System.Text.Json.Serialization.JsonIgnore] public virtual Usuario Propietario { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore] public virtual TiposVehiculo TipoVehiculo { get; set; } = null!;
}
