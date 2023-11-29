using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UnidadAsignada
    {
        public int IdUnidadAsignada { get; set; }   

        public DateTime FechaSolicitud { get; set; }    

        public string? Solicitud { get; set; }   

        public bool Correct { get; set; }   

        public List<Object>? Objects { get; set; }


        public static UnidadAsignada GetAll()
        {
            UnidadAsignada result = new UnidadAsignada(); 
            
            try
            {
                using(DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = (from OUnidad in context.UnidadAsignada
                                 select new
                                 {
                                     IdUnidadAsignada = OUnidad.IdUnidadAsignada,
                                     FechaSolicitud = OUnidad.FechaSolicitud,
                                     Solicitud = OUnidad.Solicitud

                                 }).ToList();

                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        foreach(var registro in query)
                        {
                            UnidadAsignada unidad = new UnidadAsignada();

                            unidad.IdUnidadAsignada = registro.IdUnidadAsignada;

                            unidad.FechaSolicitud = Convert.ToDateTime(registro.FechaSolicitud);

                            unidad.Solicitud = registro.Solicitud;

                            result.Objects.Add(unidad); 
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




    }
}
