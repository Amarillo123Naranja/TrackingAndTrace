using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EstatusEntrega
    {
        public int IdEstatusEntrega { get; set; }   

        public string Estatus { get; set; }

        public List<Object> Objects { get; set; }   

        public bool Correct { get; set; }   





        public static EstatusEntrega GetAll()
        {
            EstatusEntrega result = new EstatusEntrega();

            try
            {
                using (DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {

                    var query = (from OEstatusEntrega in context.EstatusEntregas
                                 select new
                                 {

                                     IdEstatusEntrega = OEstatusEntrega.IdEstatusEntrega,
                                     Estatus = OEstatusEntrega.Estatus

                                 }).ToList();
                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var registro in query)
                        {
                            EstatusEntrega entrega = new EstatusEntrega();

                            entrega.IdEstatusEntrega = registro.IdEstatusEntrega;

                            entrega.Estatus = registro.Estatus;

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

    }
}
