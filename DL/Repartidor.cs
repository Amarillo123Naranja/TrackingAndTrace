using System;
using System.Collections.Generic;

namespace DL;

public partial class Repartidor
{
    public int IdRepartidor { get; set; }

    public string? Telefono { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public string? Fotografia { get; set; }

    public int? IdUnidadAsignada { get; set; }

    public int? IdEntrega { get; set; }

    public int? IdUnidad { get; set; }

    public int? IdUsuario { get; set; }

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();

    public virtual Entrega? IdEntregaNavigation { get; set; }

    public virtual UnidadAsignadum? IdUnidadAsignadaNavigation { get; set; }

    public virtual UnidadEntrega? IdUnidadNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
