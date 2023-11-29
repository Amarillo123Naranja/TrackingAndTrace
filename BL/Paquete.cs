using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Paquete
    {
        public int IdPaquete { get; set; }  

        public string Detalle { get; set; } 

        public float Peso { get; set; } 

        public string DireccionOrigen { get; set; }

        public string DireccionEntrega { get; set; }    

        public DateTime FechaEstimadaEntrega { get; set; }

        public string CodigoRastreo { get; set; }

        public bool Correct { get; set; }   

        public List<Object> Objects { get; set; }

        public Object Object { get; set; }  

        public Entrega Entrega { get; set; }

        


        //Entity Framework
        public static Paquete GetByIdCodigoRastreo(string CodigoRastreo)
        {
            Paquete result = new Paquete();

            try
            {
                using(DL.TrackingAndTraceContext context =  new DL.TrackingAndTraceContext())
                {
                    var query = context.Paquetes.FromSqlRaw($"CodigoRastreoGetById '{CodigoRastreo}'").AsEnumerable().SingleOrDefault();

                    result.Object = new List<Object>();

                    if (query != null)
                    {
                       
                            Paquete paquete = new Paquete();

                            paquete.IdPaquete = query.IdPaquete;

                            paquete.Detalle = query.Detalle;

                            paquete.Peso = (float)query.Peso;

                            paquete.DireccionOrigen = query.DireccionOrigen;

                            paquete.DireccionEntrega = query.DireccionEntrega;

                            paquete.FechaEstimadaEntrega = Convert.ToDateTime(query.FechaEstimadaEntrega);

                            paquete.CodigoRastreo = query.CodigoRastreo;

                            paquete.Entrega = new Entrega();

                            paquete.Entrega.IdEntrega = query.IdEntrega.Value;

                            result.Object = paquete;

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

        //LINQ
        public static Paquete Add(Paquete paquete)
        {
            Paquete result = new Paquete();

            try
            {
                using(DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"PaqueteAdd '{paquete.Detalle}', {paquete.Peso}, '{paquete.DireccionOrigen}', '{paquete.DireccionEntrega}', {paquete.Entrega.IdEntrega}");

                    if(filasAfectadas > 0)
                    {
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

        public static Paquete GetById(int IdPaquete)
        {
            Paquete result = new Paquete();
            try
            {
                using(DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = (from OPaquete in context.Paquetes
                                 join OEntrega in context.Entregas
                                 on OPaquete.IdEntrega equals OEntrega.IdEntrega
                                 where OPaquete.IdPaquete == IdPaquete
                                 select new
                                 {
                                     IdPaquete = OPaquete.IdPaquete,
                                     Detalle = OPaquete.Detalle,
                                     Peso = OPaquete.Peso,
                                     DireccionOrigen = OPaquete.DireccionOrigen,
                                     DireccionEntrega = OPaquete.DireccionEntrega,
                                     FechaEstimadaEntrega = OPaquete.FechaEstimadaEntrega,
                                     CodigoRastreo = OPaquete.CodigoRastreo,
                                     IdEntrega = OEntrega.IdEntrega,
                                     FechaEntrega = OEntrega.FechaEntrega

                                 }).SingleOrDefault();

                    result.Object = new List<Object>();

                    if (query != null)
                    {
                        Paquete paquete = new Paquete();

                        paquete.IdPaquete = query.IdPaquete;

                        paquete.Detalle = query.Detalle;

                        paquete.Peso = (float)(query.Peso);

                        paquete.DireccionOrigen = query.DireccionOrigen;

                        paquete.DireccionEntrega = query.DireccionEntrega;

                        paquete.FechaEstimadaEntrega = Convert.ToDateTime(query.FechaEstimadaEntrega);

                        paquete.CodigoRastreo = query.CodigoRastreo;

                        paquete.Entrega = new Entrega();

                        paquete.Entrega.IdEntrega = query.IdEntrega;

                        paquete.Entrega.FechaEntrega = Convert.ToDateTime(query.FechaEntrega);

                        result.Object = paquete;

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
                    
            }
            catch(Exception ex) 
            {
                result.Correct = false;
            
            
            }

            return result;  
        }
        
        public static Paquete GetAll()
        {
            Paquete result = new Paquete(); 
            
            try
            {
                using(DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = (from OPaquete in context.Paquetes
                                 join OEntrega in context.Entregas
                                 on OPaquete.IdEntrega equals OEntrega.IdEntrega
                                 select new
                                 {
                                     IdPaquete = OPaquete.IdPaquete,
                                     Detalle = OPaquete.Detalle,
                                     Peso = OPaquete.Peso,
                                     DireccionOrigen = OPaquete.DireccionOrigen,
                                     DireccionEntrega = OPaquete.DireccionEntrega,
                                     FechaEstimadaEntrega = OPaquete.FechaEstimadaEntrega,
                                     CodigoRastreo = OPaquete.CodigoRastreo,
                                     IdEntrega = OEntrega.IdEntrega,
                                     FechaEntrega = OEntrega.FechaEntrega

                                 }).ToList();

                    result.Objects = new List<Object>();
                    if(query != null)
                    {
                        foreach (var registro in query)
                        {
                            Paquete paquete = new Paquete();

                            paquete.IdPaquete = registro.IdPaquete;

                            paquete.Detalle = registro.Detalle;

                            paquete.Peso = (float)registro.Peso;

                            paquete.DireccionOrigen = registro.DireccionOrigen;

                            paquete.DireccionEntrega = registro.DireccionEntrega;

                            paquete.FechaEstimadaEntrega = Convert.ToDateTime(registro.FechaEstimadaEntrega);

                            paquete.CodigoRastreo = registro.CodigoRastreo;

                            paquete.Entrega = new Entrega();

                            paquete.Entrega.IdEntrega = registro.IdEntrega;

                            paquete.Entrega.FechaEntrega = Convert.ToDateTime(registro.FechaEntrega);

                            result.Objects.Add(paquete);

                        }

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false; 
                    }
                }

            }
            catch(Exception ex) 
            {
                result.Correct = false;
            
            }

            return result;  
        }


        public static Paquete PaqueteGetAll(string nombre, int idEstatusEntrega)
        {

            Paquete result = new Paquete();

            try
            {
                using (DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = context.Paquetes.FromSqlRaw($"PaqueteGetAll '{nombre}', {idEstatusEntrega}").ToList();
                                 
                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var registro in query)
                        {
                            Paquete paquete = new Paquete();

                            paquete.IdPaquete = registro.IdPaquete;

                            paquete.Peso = (float)registro.Peso;

                            paquete.Detalle = registro.Detalle;

                            paquete.DireccionOrigen = registro.DireccionOrigen;

                            paquete.DireccionEntrega = registro.DireccionEntrega;

                            paquete.FechaEstimadaEntrega = Convert.ToDateTime(registro.FechaEstimadaEntrega);

                            paquete.CodigoRastreo = registro.CodigoRastreo;

                            paquete.Entrega = new Entrega();



                            paquete.Entrega.EstatusEntrega = new EstatusEntrega();

                            paquete.Entrega.EstatusEntrega.IdEstatusEntrega = registro.IdEstatusEntrega;

                            paquete.Entrega.EstatusEntrega.Estatus = registro.Estatus;

                            paquete.Entrega.Repartidor = new Repartidor();

                            paquete.Entrega.Repartidor.IdRepartidor = registro.IdRepartidor;

                            paquete.Entrega.Repartidor.Fotografia = registro.Fotografia;



                            paquete.Entrega.Repartidor.Usuario = new Usuario();

                            paquete.Entrega.Repartidor.Usuario.IdUsuario = registro.IdUsuario;

                            paquete.Entrega.Repartidor.Usuario.Nombre = registro.Nombre;

                            paquete.Entrega.Repartidor.Usuario.ApellidoPaterno = registro.ApellidoPaterno;

                            paquete.Entrega.Repartidor.Usuario.ApellidoMaterno = registro.ApellidoMaterno;

                            result.Objects.Add(paquete);


                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }


                }

            }
            catch (Exception e)
            {

                result.Correct = false;

            }

            return result;
        }



        //public static Paquete PaqueteGetAll(string Nombre, int IdEstatusEntrega)
        //{

        //    Paquete result = new Paquete();

        //    try
        //    {
        //        using (DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
        //        {
        //            var query = (from OPaquete in context.Paquetes
        //                         join OEntrega in context.Entregas
        //                         on OPaquete.IdEntrega equals OEntrega.IdEntrega
        //                         join OEstatusEntrega in context.EstatusEntregas
        //                         on OEntrega.IdEstatusEntrega equals OEstatusEntrega.IdEstatusEntrega
        //                         join ORepartidor in context.Repartidors
        //                         on OEntrega.IdRepartidor equals ORepartidor.IdRepartidor
        //                         join OUsuario in context.Usuarios
        //                         on ORepartidor.IdUsuario equals OUsuario.IdUsuario
        //                         join ORol in context.Rols
        //                         on OUsuario.IdRol equals ORol.IdRol
        //                         where OUsuario.Nombre == Nombre
        //                         && OEstatusEntrega.IdEstatusEntrega == IdEstatusEntrega
        //                         select new
        //                         {
        //                             IdPaquete = OPaquete.IdPaquete,
        //                             Peso = OPaquete.Peso,
        //                             Detalle = OPaquete.Detalle,
        //                             DireccionOrigen = OPaquete.DireccionOrigen,
        //                             DireccionEntrega = OPaquete.DireccionEntrega,
        //                             FechaEstimadaEntrega = OPaquete.FechaEstimadaEntrega,
        //                             CodigoRastreo = OPaquete.CodigoRastreo,
        //                             IdEstatusEntrega = OEstatusEntrega.IdEstatusEntrega,   
        //                             Estatus = OEstatusEntrega.Estatus,
        //                             IdRepartidor = ORepartidor.IdRepartidor,
        //                             Fotografia = ORepartidor.Fotografia,
        //                             IdUsuario = OUsuario.IdUsuario,
        //                             Nombre = OUsuario.Nombre,
        //                             ApellidoPaterno = OUsuario.ApellidoPaterno,
        //                             ApellidoMaterno = OUsuario.ApellidoMaterno

        //                         }).ToList();

        //            result.Objects = new List<Object>();

        //            if (query != null)
        //            {
        //                foreach (var registro in query)
        //                {
        //                    Paquete paquete = new Paquete();

        //                    paquete.IdPaquete = registro.IdPaquete;

        //                    paquete.Peso = (float)registro.Peso;

        //                    paquete.Detalle = registro.Detalle;

        //                    paquete.DireccionOrigen = registro.DireccionOrigen;

        //                    paquete.DireccionEntrega = registro.DireccionEntrega;

        //                    paquete.FechaEstimadaEntrega = Convert.ToDateTime(registro.FechaEstimadaEntrega);

        //                    paquete.CodigoRastreo = registro.CodigoRastreo;

        //                    paquete.Entrega = new Entrega();



        //                    paquete.Entrega.EstatusEntrega = new EstatusEntrega();

        //                    paquete.Entrega.EstatusEntrega.IdEstatusEntrega = registro.IdEstatusEntrega;

        //                    paquete.Entrega.EstatusEntrega.Estatus = registro.Estatus;

        //                    paquete.Entrega.Repartidor = new Repartidor();

        //                    paquete.Entrega.Repartidor.IdRepartidor = registro.IdRepartidor;

        //                    paquete.Entrega.Repartidor.Fotografia = registro.Fotografia;



        //                    paquete.Entrega.Repartidor.Usuario = new Usuario();

        //                    paquete.Entrega.Repartidor.Usuario.IdUsuario = registro.IdUsuario;  

        //                    paquete.Entrega.Repartidor.Usuario.Nombre = registro.Nombre;

        //                    paquete.Entrega.Repartidor.Usuario.ApellidoPaterno = registro.ApellidoPaterno;

        //                    paquete.Entrega.Repartidor.Usuario.ApellidoMaterno = registro.ApellidoMaterno;

        //                    result.Objects.Add(paquete);


        //                }

        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //            }


        //        }

        //    }
        //    catch (Exception e)
        //    {

        //        result.Correct = false;

        //    }

        //    return result;
        //}

    }
}
