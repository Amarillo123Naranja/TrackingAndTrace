using System;
using System.Collections.Generic;

namespace DL;

public partial class EstatusUnidad
{
    public int IdEstatusUnidad { get; set; }

    public bool? Estatus { get; set; }

    public virtual ICollection<UnidadEntrega> UnidadEntregas { get; set; } = new List<UnidadEntrega>();
}
