using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EstatusUnidad
    {
        public int? IdEstatusUnidad { get; set; }
        
        public bool? Estatus { get; set; }


        public List<Object>? Objects { get; set; }

        public bool? Correct { get; set; }


        public static EstatusUnidad GetAll()
        {
            EstatusUnidad result = new EstatusUnidad();

            try
            {
                using (DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = context.EstatusUnidads.FromSqlRaw("EstatusUnidadGetAll").ToList();

                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var registro in query)
                        {
                            EstatusUnidad unidad = new EstatusUnidad();

                            unidad.IdEstatusUnidad = registro.IdEstatusUnidad;

                            unidad.Estatus = Convert.ToBoolean(registro.Estatus);

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
            catch (Exception ex)
            {
                result.Correct = false;

            }

            return result;

        }
    

    }

   
}
