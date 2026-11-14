using System;
using System.Collections.Generic;

namespace UP.Infrastructure.Models;

public partial class Vehiculo
{
    public int VehiculoId { get; set; }

    public int PropietarioId { get; set; }

    public string Placa { get; set; } = null!;

    public int TipoVehiculoId { get; set; }

    public virtual ICollection<Entrada> Entrada { get; set; } = new List<Entrada>();

    public virtual Usuario Propietario { get; set; } = null!;

    public virtual TiposVehiculo TipoVehiculo { get; set; } = null!;
}
