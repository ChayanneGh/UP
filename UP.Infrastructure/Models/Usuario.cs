using System;
using System.Collections.Generic;

namespace P4rquing.Infraestructure.Models;

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

    public virtual ICollection<Entrada> Entrada { get; set; } = new List<Entrada>();

    public virtual ICollection<Reservacione> Reservaciones { get; set; } = new List<Reservacione>();

    public virtual Role? Rol { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
