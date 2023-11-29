namespace BL
{
    public class Repartidor
    {
        public int IdRepartidor { get; set; } 

        public string? Telefono { get; set; }    

        public DateTime FechaIngreso { get; set; }  

        public string? Fotografia { get; set; }  

        //Propiedad de navegacion IdUnidadAsignada

        public BL.UnidadAsignada UnidadAsignada { get; set; }   

        public BL.UnidadEntrega UnidadEntrega { get; set; }

        public BL.Entrega Entrega { get; set; } 
    

        public BL.Usuario Usuario { get; set; }
        public List<Object>? Objects { get; set; }   

        public Object? Object { get; set; }

        public bool Correct { get; set; }   

        public static Repartidor GetAll()
        {
            Repartidor result = new Repartidor();

            try
            {
                using(DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = (from ORepartidor in context.Repartidors
                                 join OUnidadEntrega in context.UnidadEntregas 
                                 on ORepartidor.IdUnidad equals OUnidadEntrega.IdUnidad
                                 join OUsuario in context.Usuarios
                                 on ORepartidor.IdUsuario equals OUsuario.IdUsuario
                                 select new
                                 {
                                    
                                     IdUsuario = OUsuario.IdUsuario,
                                     Nombre = OUsuario.Nombre,
                                     ApellidoPaterno = OUsuario.ApellidoPaterno,
                                     ApellidoMaterno = OUsuario.ApellidoMaterno,
                                     IdRepartidor = ORepartidor.IdRepartidor,
                                     Telefono = ORepartidor.Telefono,
                                     FechaIngreso = ORepartidor.FechaIngreso,
                                     Fotografia = ORepartidor.Fotografia,
                                     IdUnidad = OUnidadEntrega.IdUnidad
    

                                 }).ToList();

                    result.Objects = new List<Object>();
                    if(query != null)
                    {
                        foreach(var registro in query)
                        {
                            Repartidor repartidor = new Repartidor();

                            repartidor.Usuario = new Usuario();

                            repartidor.Usuario.IdUsuario = registro.IdUsuario;

                            repartidor.Usuario.Nombre = registro.Nombre;    

                            repartidor.Usuario.ApellidoPaterno = registro.ApellidoPaterno;

                            repartidor.Usuario.ApellidoMaterno = registro.ApellidoMaterno;

                            repartidor.IdRepartidor = registro.IdRepartidor;

                            repartidor.Telefono = registro.Telefono;

                            repartidor.FechaIngreso = Convert.ToDateTime(registro.FechaIngreso);

                            repartidor.Fotografia = registro.Fotografia;

                            repartidor.UnidadEntrega = new UnidadEntrega();

                            repartidor.UnidadEntrega.IdUnidad = registro.IdUnidad;

         
                            result.Objects.Add(repartidor);
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

        public static Repartidor GetById(int id)
        {
            Repartidor result = new Repartidor();

            try
            {
                using(DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = (from ORepartidor in context.Repartidors
                                 join OUnidadEntrega in context.UnidadEntregas
                                 on ORepartidor.IdUnidad equals OUnidadEntrega.IdUnidad
                                 join OUsuario in context.Usuarios
                                 on ORepartidor.IdUsuario equals OUsuario.IdUsuario  
                                 join OUnidadAsignada in context.UnidadAsignada
                                 on ORepartidor.IdUnidadAsignada equals OUnidadAsignada.IdUnidadAsignada
                                 join OEntrega in context.Entregas
                                 on ORepartidor.IdEntrega equals OEntrega.IdEntrega
                                 where ORepartidor.IdRepartidor == id
                                 select new
                                 {
                                     IdUsuario = OUsuario.IdUsuario,
                                     Nombre = OUsuario.Nombre,  
                                     ApellidoPaterno = OUsuario.ApellidoPaterno,
                                     ApellidoMaterno = OUsuario.ApellidoMaterno,
                                     IdRepartidor = ORepartidor.IdRepartidor,
                                     Telefono = ORepartidor.Telefono,
                                     FechaIngreso = ORepartidor.FechaIngreso,
                                     Fotografia = ORepartidor.Fotografia,
                                     IdUnidad = OUnidadEntrega.IdUnidad,
                                     NumeroPlaca = OUnidadEntrega.NumeroPlaca,
                                     Modelo = OUnidadEntrega.Modelo,
                                     Marca = OUnidadEntrega.Marca,
                                     AñoFabricacion = OUnidadEntrega.AñoFabricacion,
                                     IdUnidadAsignada = OUnidadAsignada.IdUnidadAsignada,
                                     FechaSolicitud = OUnidadAsignada.FechaSolicitud,
                                     IdEntrega = OEntrega.IdEntrega,
                                     FechaEntrega = OEntrega.FechaEntrega

                                     

                                 }).SingleOrDefault();

                    result.Object = new List<Object>();

                    if (query != null)
                    {
                        Repartidor repartidor = new Repartidor();

                        repartidor.Usuario = new Usuario();

                        repartidor.Usuario.IdUsuario = query.IdUsuario;

                        repartidor.Usuario.Nombre = query.Nombre;

                        repartidor.Usuario.ApellidoPaterno = query.ApellidoPaterno;

                        repartidor.Usuario.ApellidoMaterno = query.ApellidoMaterno;

                        repartidor.IdRepartidor = query.IdRepartidor;

                        repartidor.Telefono = query.Telefono;

                        repartidor.FechaIngreso = Convert.ToDateTime(query.FechaIngreso);

                        repartidor.Fotografia = query.Fotografia;

                        repartidor.UnidadEntrega = new UnidadEntrega();

                        repartidor.UnidadEntrega.IdUnidad = query.IdUnidad;

                        repartidor.UnidadEntrega.NumeroPlaca = query.NumeroPlaca;   

                        repartidor.UnidadEntrega.Modelo = query.Modelo; 

                        repartidor.UnidadEntrega.Marca = query.Marca;

                        repartidor.UnidadEntrega.AñoFabricacion = query.AñoFabricacion;

                        repartidor.UnidadAsignada = new UnidadAsignada();

                        repartidor.UnidadAsignada.IdUnidadAsignada = query.IdUnidadAsignada;

                        repartidor.UnidadAsignada.FechaSolicitud = Convert.ToDateTime(query.FechaSolicitud);

                        repartidor.Entrega  = new Entrega();

                        repartidor.Entrega.IdEntrega = query.IdEntrega;

                        repartidor.Entrega.FechaEntrega = Convert.ToDateTime(query.FechaEntrega);



                        result.Object = repartidor;

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

        public static Repartidor GetByIdUnidadEntrega(int id)
        {
            Repartidor result = new Repartidor();

            try
            {
                using (DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = (from ORepartidor in context.Repartidors
                                 join OUnidadEntrega in context.UnidadEntregas
                                 on ORepartidor.IdUnidad equals OUnidadEntrega.IdUnidad
                                 join OUsuario in context.Usuarios
                                 on ORepartidor.IdUsuario equals OUsuario.IdUsuario
                                 where ORepartidor.IdUsuario== id
                                 select new
                                 {
                                     IdUsuario = OUsuario.IdUsuario,
                                     Nombre = OUsuario.Nombre,
                                     ApellidoPaterno = OUsuario.ApellidoPaterno,
                                     ApellidoMaterno = OUsuario.ApellidoMaterno,
                                     IdRepartidor = ORepartidor.IdRepartidor,
                                     Telefono = ORepartidor.Telefono,
                                     FechaIngreso = ORepartidor.FechaIngreso,
                                     Fotografia = ORepartidor.Fotografia,
                                     IdUnidad = OUnidadEntrega.IdUnidad,
                                     NumeroPlaca = OUnidadEntrega.NumeroPlaca,
                                     Modelo = OUnidadEntrega.Modelo,
                                     Marca = OUnidadEntrega.Marca,
                                     AñoFabricacion = OUnidadEntrega.AñoFabricacion,
                                   

                                 }).SingleOrDefault();

                    result.Object = new List<Object>();

                    if (query != null)
                    {
                        Repartidor repartidor = new Repartidor();

                        repartidor.Usuario = new Usuario();

                        repartidor.Usuario.IdUsuario = query.IdUsuario;

                        repartidor.Usuario.Nombre = query.Nombre;

                        repartidor.Usuario.ApellidoPaterno = query.ApellidoPaterno;

                        repartidor.Usuario.ApellidoMaterno = query.ApellidoMaterno;

                        repartidor.IdRepartidor = query.IdRepartidor;

                        repartidor.Telefono = query.Telefono;

                        repartidor.FechaIngreso = Convert.ToDateTime(query.FechaIngreso);

                        repartidor.Fotografia = query.Fotografia;

                        repartidor.UnidadEntrega = new UnidadEntrega();

                        repartidor.UnidadEntrega.IdUnidad = query.IdUnidad;

                        repartidor.UnidadEntrega.NumeroPlaca = query.NumeroPlaca;

                        repartidor.UnidadEntrega.Modelo = query.Modelo;

                        repartidor.UnidadEntrega.Marca = query.Marca;

                        repartidor.UnidadEntrega.AñoFabricacion = query.AñoFabricacion;

                       

                        result.Object = repartidor;

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


        public static Repartidor Add(Repartidor repartidor)
        {
            Repartidor result = new Repartidor();

            try
            {
                using(DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    DL.Repartidor repartidorEntity = new DL.Repartidor();

                    repartidorEntity.Telefono = repartidor.Telefono;

                    repartidorEntity.FechaIngreso = repartidor.FechaIngreso;

                    repartidorEntity.Fotografia = repartidor.Fotografia;

                    repartidorEntity.IdUnidadAsignada = repartidor.UnidadAsignada.IdUnidadAsignada;

                    repartidorEntity.IdEntrega = repartidor.Entrega.IdEntrega;

                    repartidorEntity.IdUnidad = repartidor.UnidadEntrega.IdUnidad;

                    repartidorEntity.IdUsuario = repartidor.Usuario.IdUsuario;

                    context.Repartidors.Add(repartidorEntity);

                    int filasAfectadas = context.SaveChanges();

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



        public static Repartidor Update(Repartidor repartidor)
        {
            Repartidor result = new Repartidor();

            try
            {
                using(DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = (from ORepartidor in context.Repartidors
                                 where ORepartidor.IdRepartidor == repartidor.IdRepartidor
                                 select ORepartidor).SingleOrDefault();

                    if (query != null)
                    {

                        query.Telefono = repartidor.Telefono;

                        query.FechaIngreso = repartidor.FechaIngreso;

                        query.Fotografia = repartidor.Fotografia;

                        query.IdUnidadAsignada = repartidor.UnidadAsignada.IdUnidadAsignada;

                        query.IdEntrega = repartidor.Entrega.IdEntrega; 

                        query.IdUnidad = repartidor.UnidadEntrega.IdUnidad;

                        query.IdUsuario = repartidor.Usuario.IdUsuario;

                        int filasAfectadas = context.SaveChanges();

                        if(filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false; 
                        }


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



        public static Repartidor Delete(int IdRepartidor)
        {
            Repartidor result = new Repartidor(); 
            
            try
            {
                using(DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = (from ORepartidor in context.Repartidors
                                 where ORepartidor.IdRepartidor == IdRepartidor
                                 select ORepartidor).First();

                    context.Repartidors.Remove(query);

                    int filasAfectadas = context.SaveChanges();

                    if( filasAfectadas > 0)
                    {
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