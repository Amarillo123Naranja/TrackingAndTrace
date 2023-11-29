using System;
using System.Collections.Generic;

namespace DL;

public partial class UnidadEntrega
{
    public int IdUnidad { get; set; }

    public int? NumeroPlaca { get; set; }

    public string? Modelo { get; set; }

    public string? Marca { get; set; }

    public string? AñoFabricacion { get; set; }

    public int? IdEstatusUnidad { get; set; }

    public virtual EstatusUnidad? IdEstatusUnidadNavigation { get; set; }

    public virtual ICollection<Repartidor> Repartidors { get; set; } = new List<Repartidor>();
}
