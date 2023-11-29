using System;
using System.Collections.Generic;

namespace DL;

public partial class Entrega
{
    public int IdEntrega { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public int? IdPaquete { get; set; }

    public int? IdRepartidor { get; set; }

    public int? IdEstatusEntrega { get; set; }

    public virtual ICollection<EstatusEntrega> EstatusEntregas { get; set; } = new List<EstatusEntrega>();

    public virtual EstatusEntrega? IdEstatusEntregaNavigation { get; set; }

    public virtual Paquete? IdPaqueteNavigation { get; set; }

    public virtual Repartidor? IdRepartidorNavigation { get; set; }

    public virtual ICollection<Paquete> Paquetes { get; set; } = new List<Paquete>();

    public virtual ICollection<Repartidor> Repartidors { get; set; } = new List<Repartidor>();

    public double Peso { set; get; }

    public string Detalle { set; get; } 

    public string DireccionOrigen { set; get; } 

    public string DireccionEntrega { set; get; }    

    public DateTime FechaEstimadaEntrega { set; get; }  

    public string CodigoRastreo { set; get; }   

   public string Estatus { set; get; }  

   public string Fotografia { set; get; }   

    public int IdUsuario { set; get; }   

    public string Nombre { set; get; }  

    public string ApellidoPaterno { set; get; } 

    public string ApellidoMaterno { set; get; } 












}
