using System;
using System.Collections.Generic;

namespace DL;

public partial class UnidadAsignadum
{
    public int IdUnidadAsignada { get; set; }

    public DateTime? FechaSolicitud { get; set; }

    public string? Solicitud { get; set; }

    public virtual ICollection<Repartidor> Repartidors { get; set; } = new List<Repartidor>();
}
