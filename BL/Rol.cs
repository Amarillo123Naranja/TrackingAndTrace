using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public int IdRol { get; set; }  

        public string? Nombre { get; set; }  

        public List<Object>? Roles { get; set; }

        public bool Correct { get; set; }


        public static Rol GetAll()
        {
            Rol result = new Rol();

            try
            {
                using(DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = context.Rols.FromSqlRaw("RolGetAll").ToList();

                    result.Roles = new List<Object>();

                    if(query != null)
                    {
                        foreach (var registro in query) 
                        {
                            Usuario usuario = new Usuario();

                            usuario.IdRol = registro.IdRol;

                            usuario.Nombre = registro.Nombre;

                            result.Roles.Add(usuario);
                        
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
