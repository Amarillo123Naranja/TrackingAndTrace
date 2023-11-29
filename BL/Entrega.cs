using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Entrega
    {
        public int IdEntrega { get; set; }
        
        public DateTime FechaEntrega { get; set; }  

        public int IdRepartidor { get; set; }   

        public int IdPaquete { get; set; }

        public int IdEstatusEntrega { get; set; }   
        public BL.Paquete Paquete { get; set; } 

        public BL.Repartidor Repartidor { get; set; }

        public BL.EstatusEntrega EstatusEntrega { get; set;}

        public List<Object> Objects { get; set; }   

        public bool Correct { get; set; }   

        public BL.Entrega Entregar { get; set; } 



        public static Entrega GetAll()
        {
            Entrega result = new Entrega();

            try
            {
                using(DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = (from OEntrega in context.Entregas
                                 select new
                                 {
                                     IdEntrega = OEntrega.IdEntrega,
                                     FechaEntrega = OEntrega.FechaEntrega

                                 }).ToList();

                    result.Objects = new List<Object>();

                    if(query != null)
                    {
                        foreach(var registro in query)
                        {
                            Entrega entrega = new Entrega();

                            entrega.IdEntrega = registro.IdEntrega;

                            entrega.FechaEntrega = Convert.ToDateTime(registro.FechaEntrega);

                            result.Objects.Add(entrega);
                        }

                        result.Correct = true;
                    }

                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; 
            }

            return result;  
        }


        //public static Paquete DetallePaquete()
        //{
        //    Paquete result = new Paquete();

        //    try
        //    {

        //        using (DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
        //        {
        //            var query = (from OEntrega in context.Entregas
        //                         join OPaquete in context.Paquetes
        //                         on OEntrega.IdPaquete equals OPaquete.IdPaquete
        //                         join OEstatusEntrega in context.EstatusEntregas
        //                         on OEntrega.IdEstatusEntrega equals OEstatusEntrega.IdEstatusEntrega
        //                         join ORepartidor in context.Repartidors
        //                         on OEntrega.IdRepartidor equals ORepartidor.IdRepartidor
        //                         join OUsuario in context.Usuarios
        //                         on ORepartidor.IdUsuario equals OUsuario.IdUsuario
        //                         select new
        //                         {
        //                             IdPaquete = OPaquete.IdPaquete,
        //                             Detalle = OPaquete.Detalle,
        //                             Peso = OPaquete.Peso,
        //                             DireccionOrigen = OPaquete.DireccionOrigen,
        //                             DireccionEntrega = OPaquete.DireccionEntrega,
        //                             FechaEstimadaEntrega = OPaquete.FechaEstimadaEntrega,
        //                             CodigoRastreo = OPaquete.CodigoRastreo,
        //                             Estatus = OEstatusEntrega.Estatus,
        //                             IdRepartidor = ORepartidor.IdRepartidor,
        //                             Telefono = ORepartidor.Telefono,
        //                             Fotografia = ORepartidor.Fotografia,
        //                             IdUsuario = OUsuario.IdUsuario,
        //                             Nombre = OUsuario.Nombre,
        //                             ApellidoPaterno = OUsuario.ApellidoPaterno,
        //                             ApellidoMaterno = OUsuario.ApellidoMaterno,
        //                             Email = OUsuario.Email

        //                         }).ToList();

        //            result.Objects = new List<Object>();

        //            if(query != null)
        //            {
        //                foreach(var registro in query)
        //                {
        //                    Entrega entrega = new Entrega();

        //                    entrega.Paquete = new Paquete();




        //                }
        //            }
        //        }

        //    }


        //}



        public static Entrega Prueba(string nombre, int idUnidad)
        {
            Entrega result = new Entrega();

            try
            {
                using (DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = context.Entregas.FromSqlRaw($"EntregaGetAll '{nombre}', {idUnidad}").ToList();

                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var registro in query)
                        {
                            Entrega paquete = new Entrega();

                            paquete.Paquete.IdPaquete = registro.IdPaquete.Value;

                            paquete.Paquete.Peso = (float)registro.Peso;

                            paquete.Paquete.Detalle = registro.Detalle;

                            paquete.Paquete.DireccionOrigen = registro.DireccionOrigen;

                            paquete.Paquete.DireccionEntrega = registro.DireccionEntrega;

                            paquete.Paquete.FechaEstimadaEntrega = Convert.ToDateTime(registro.FechaEstimadaEntrega);

                            paquete.Paquete.CodigoRastreo = registro.CodigoRastreo;

                            paquete.Entregar = new Entrega();



                            paquete.Entregar.EstatusEntrega = new EstatusEntrega();

                            paquete.Entregar.EstatusEntrega.IdEstatusEntrega = registro.IdEstatusEntrega.Value;

                            paquete.Entregar.EstatusEntrega.Estatus = registro.Estatus;

                            paquete.Entregar.Repartidor = new Repartidor();

                            paquete.Entregar.Repartidor.IdRepartidor = registro.IdRepartidor.Value;

                            paquete.Entregar.Repartidor.Fotografia = registro.Fotografia;



                            //paquete.Entregar.Repartidor.Usuario = new Usuario();

                            //paquete.Entregar.Repartidor.Usuario.IdUsuario = registro.IdUsuario;

                            //paquete.Entregar.Repartidor.Usuario.Nombre = registro.Nombre;

                            //paquete.Entregar.Repartidor.Usuario.ApellidoPaterno = registro.ApellidoPaterno;

                            //paquete.Entregar.Repartidor.Usuario.ApellidoMaterno = registro.ApellidoMaterno;

                            result.Objects.Add(paquete);






                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
            }

            return result;

        }


    }
}
