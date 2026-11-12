using System;
using System.Collections.Generic;

namespace P4rquing.Infraestructure.Models;

public partial class Entrada
{
    public int EntradaId { get; set; }

    public DateTime? HoraEntrada { get; set; }

    public int EspacioId { get; set; }

    public int UsuarioId { get; set; }

    public string Placa { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual Estacionamiento Espacio { get; set; } = null!;

    public virtual Vehiculo PlacaNavigation { get; set; } = null!;

    public virtual ICollection<Salida> Salida { get; set; } = new List<Salida>();

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();

    public virtual Usuario Usuario { get; set; } = null!;
}
