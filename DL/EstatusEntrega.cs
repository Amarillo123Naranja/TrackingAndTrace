using System;
using System.Collections.Generic;

namespace DL;

public partial class EstatusEntrega
{
    public int IdEstatusEntrega { get; set; }

    public string? Estatus { get; set; }

    public int? IdEntrega { get; set; }

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();

    public virtual Entrega? IdEntregaNavigation { get; set; }
}
