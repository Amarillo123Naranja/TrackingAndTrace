using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public int? IdRol { get; set; }

    public string NombreRol { get; set; }   

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<Repartidor> Repartidors { get; set; } = new List<Repartidor>();
}
