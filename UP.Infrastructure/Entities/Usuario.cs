using System;
using System.Collections.Generic;

namespace UP.Infrastructure.Entities;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public int? RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Rne { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public bool? EsActivo { get; set; }

    [System.Text.Json.Serialization.JsonIgnore] public virtual ICollection<Entrada> Entrada { get; set; } = new List<Entrada>();

    [System.Text.Json.Serialization.JsonIgnore] public virtual ICollection<Reservacione> Reservaciones { get; set; } = new List<Reservacione>();

    [System.Text.Json.Serialization.JsonIgnore] public virtual Role? Rol { get; set; }

    [System.Text.Json.Serialization.JsonIgnore] public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();

    [System.Text.Json.Serialization.JsonIgnore] public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
