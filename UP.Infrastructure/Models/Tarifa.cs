using System;
using System.Collections.Generic;

namespace P4rquing.Infraestructure.Models;

public partial class Tarifa
{
    public int TarifaId { get; set; }

    public decimal TarifaHora { get; set; }

    public decimal TarifaDia { get; set; }

    public DateTime VigenciaInicio { get; set; }

    public bool? EsActiva { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
