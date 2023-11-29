using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UnidadEntrega
    {
        public int IdUnidad { get; set; }  

        public int? NumeroPlaca { get; set; }    

        public string? Modelo { get; set; }  

        public string? Marca { get; set; }   

        public string? AñoFabricacion { get; set; }  

        //Propiedad de navegacion IdEstatusUnidad

        public BL.EstatusUnidad EstatusUnidad { get; set; }

        public bool Correct { get; set; }   

        public Object? Object { get; set; }

        public List <Object>? Objects { get; set; }



        public static UnidadEntrega Add(UnidadEntrega entrega)
        {
            UnidadEntrega result = new UnidadEntrega();
            
            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "UnidadEntregaAdd";

                    SqlCommand cmd = new SqlCommand(query, context);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[5];

                    collection[0] = new SqlParameter("@NumeroPlaca", SqlDbType.Int);
                    collection[0].Value = entrega.NumeroPlaca;

                    collection[1] = new SqlParameter("@Modelo", SqlDbType.VarChar);
                    collection[1].Value = entrega.Modelo;

                    collection[2] = new SqlParameter("@Marca", SqlDbType.VarChar);
                    collection[2].Value = entrega.Marca;

                    collection[3] = new SqlParameter("@AñoFabricacion", SqlDbType.VarChar);
                    collection[3].Value = entrega.AñoFabricacion;

                    collection[4] = new SqlParameter("@IdEstatusUnidad", SqlDbType.Bit);
                    collection[4].Value = entrega.EstatusUnidad.IdEstatusUnidad;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery(); 

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

        public static UnidadEntrega Update(UnidadEntrega entrega)
        {
            UnidadEntrega result = new UnidadEntrega();

            try
            {
                using(SqlConnection contex = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "UnidadEntregaUpdate";

                    SqlCommand cmd = new SqlCommand(query, contex);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[6];

                    collection[0] = new SqlParameter("@IdUnidad", SqlDbType.Int);
                    collection[0].Value = entrega.IdUnidad;

                    collection[1] = new SqlParameter("@NumeroPlaca", SqlDbType.Int);
                    collection[1].Value = entrega.NumeroPlaca;

                    collection[2] = new SqlParameter("@Modelo", SqlDbType.VarChar);
                    collection[2].Value = entrega.Modelo;

                    collection[3] = new SqlParameter("@Marca", SqlDbType.VarChar);
                    collection[3].Value = entrega.Marca;

                    collection[4] = new SqlParameter("@AñoFabricacion", SqlDbType.VarChar);
                    collection[4].Value = entrega.AñoFabricacion;

                    collection[5] = new SqlParameter("@IdEstatusUnidad", SqlDbType.Bit);
                    collection[5].Value = entrega.EstatusUnidad.IdEstatusUnidad;

                    cmd.Parameters.AddRange(collection);

                    cmd.Connection.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

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

        public static UnidadEntrega GetAll()
        {
            UnidadEntrega result = new UnidadEntrega(); 
            
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "UnidadEntregaGetAll";

                    SqlCommand cmd = new SqlCommand(query, context);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable tablaUnidad = new DataTable();

                    adapter.Fill(tablaUnidad);

                    if(tablaUnidad.Rows.Count > 0)
                    {
                        result.Objects = new List<Object>();

                        foreach(DataRow row in tablaUnidad.Rows)
                        {
                            UnidadEntrega entrega = new UnidadEntrega();

                            entrega.IdUnidad = int.Parse(row[0].ToString());

                            entrega.NumeroPlaca = int.Parse(row[1].ToString());

                            entrega.Modelo = row[2].ToString();

                            entrega.Marca = row[3].ToString();

                            entrega.AñoFabricacion = row[4].ToString();

                            entrega.EstatusUnidad = new EstatusUnidad();

                            entrega.EstatusUnidad.IdEstatusUnidad = int.Parse(row[5].ToString());

                            entrega.EstatusUnidad.Estatus = Convert.ToBoolean(row[6].ToString());

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
            catch(Exception ex) 
            {
                result.Correct = false;
            
            }

            return result;
        }


        public static UnidadEntrega GetById(int IdUnidad) 
        {
            UnidadEntrega result = new UnidadEntrega(); 
            
            try 
            {
                using(SqlConnection context =  new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "UnidadEntregaGetById";

                    SqlCommand cmd = new SqlCommand(query, context);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[1];

                    collection[0] = new SqlParameter("@IdUnidad", SqlDbType.Int);
                    collection[0].Value = IdUnidad;

                    cmd.Parameters.AddRange(collection);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable tablaUnidad = new DataTable();

                    adapter.Fill(tablaUnidad);

                    if(tablaUnidad.Rows.Count > 0)
                    {
                        DataRow row = tablaUnidad.Rows[0];

                        UnidadEntrega entrega = new UnidadEntrega();

                        entrega.IdUnidad = int.Parse(row[0].ToString());

                        entrega.NumeroPlaca = int.Parse(row[1].ToString());

                        entrega.Modelo = row[2].ToString();

                        entrega.Marca = row[3].ToString();

                        entrega.AñoFabricacion = row[4].ToString();

                        entrega.EstatusUnidad = new EstatusUnidad();

                        entrega.EstatusUnidad.IdEstatusUnidad = int.Parse(row[5].ToString());

                        entrega.EstatusUnidad.Estatus = Convert.ToBoolean(row[6].ToString());

                        result.Object = entrega;

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


        public static UnidadEntrega Delete(int IdUnidad)
        {
            UnidadEntrega result = new UnidadEntrega();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "UnidadEntregaDelete";

                    SqlCommand cmd = new SqlCommand(query, context);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[1];

                    collection[0] = new SqlParameter("@IdUnidad", SqlDbType.Int);
                    collection[0].Value = IdUnidad;

                    cmd.Parameters.AddRange(collection);

                    cmd.Connection.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

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
            catch(Exception ex) 
            {
                result.Correct = false; 
             
            }

            return result;  
            }






        } 


    }
