using System;
using System.Collections.Generic;

namespace DL;

public partial class Paquete
{
    public int IdPaquete { get; set; }

    public string? Detalle { get; set; }

    public double? Peso { get; set; }

    public string? DireccionOrigen { get; set; }

    public string? DireccionEntrega { get; set; }

    public DateTime? FechaEstimadaEntrega { get; set; }

    public string? CodigoRastreo { get; set; }

    public int? IdEntrega { get; set; }

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();

    public virtual Entrega? IdEntregaNavigation { get; set; }



    public string Estatus { set; get; }

    public string Fotografia { set; get; }

    public int IdUsuario { set; get; }

    public string Nombre { set; get; }

    public string ApellidoPaterno { set; get; }

    public string ApellidoMaterno { set; get; }

    public int IdEstatusEntrega { set; get; }   

    public int IdRepartidor { set; get; }   





}
