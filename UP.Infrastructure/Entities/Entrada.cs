using System;
using System.Collections.Generic;

namespace UP.Infrastructure.Entities;

public partial class Entrada
{
    public int EntradaId { get; set; }

    public DateTime? HoraEntrada { get; set; }

    public int EspacioId { get; set; }

    public int UsuarioId { get; set; }

    public string Placa { get; set; } = null!;

    public bool? Estado { get; set; }

    [System.Text.Json.Serialization.JsonIgnore] public virtual Estacionamiento Espacio { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore] public virtual Vehiculo PlacaNavigation { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore] public virtual ICollection<Salida> Salida { get; set; } = new List<Salida>();

    [System.Text.Json.Serialization.JsonIgnore] public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();

    [System.Text.Json.Serialization.JsonIgnore] public virtual Usuario Usuario { get; set; } = null!;
}
