using DL;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BL
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? Nombre { get; set; }

        public string? ApellidoMaterno { get; set; }

        public string? ApellidoPaterno { get; set; }

        public int? IdRol { get; set; }

        public bool Correct { get; set; }





        //Propiedad de navegacion IdRol
        public Rol Rol { get; set; }

        public List<Object> UsuariosObject { get; set; }

        public object UsuarioObject { get; set; }



        public static Usuario GetByIdEmail(string email)
        {
            Usuario result = new Usuario();

            try
            {
                using (DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                   
                    var query = context.Usuarios.FromSqlRaw($"GetByIdEmail '{email}'").AsEnumerable().SingleOrDefault();

                    result.UsuarioObject = new List<Object>();

                    if (query != null)
                    {
                        Usuario user = new Usuario();

                        user.IdUsuario = query.IdUsuario;

                        user.Email = query.Email;

                        user.Password = query.Password;

                        user.Rol = new Rol();

                        user.Rol.IdRol = Convert.ToInt16(query.IdRol);

                        user.Rol.Nombre = query.NombreRol;


                        result.UsuarioObject = user;

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

        public static Usuario Add(Usuario usuario)
        {
            Usuario result = new Usuario();

            try
            {
                using (DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    string hashedPassword = EncryptPassword(usuario.Password);
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.UserName}', '{hashedPassword}', '{usuario.Email}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', {usuario.Rol.IdRol}");

                    if (filasAfectadas > 0)
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

        public static Usuario Update(Usuario usuario)
        {
            Usuario result = new Usuario();

            try
            {
                using (DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {

                    int filasAfectadas = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario}, '{usuario.UserName}', '{usuario.Password}', '{usuario.Email}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', {usuario.Rol.IdRol}");

                    if (filasAfectadas > 0)
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

        public static Usuario Delete(int IdUsuario)
        {
            Usuario result = new Usuario();

            try
            {
                using (DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"UsuarioDelete {IdUsuario}");

                    if (filasAfectadas > 0)
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

        public static Usuario GetById(int IdUsuario)
        {
            Usuario result = new Usuario();

            try
            {
                using (DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().SingleOrDefault();

                    result.UsuarioObject = new List<Object>();

                    if (query != null)
                    {
                        Usuario usuario = new Usuario();

                        usuario.IdUsuario = query.IdUsuario;

                        usuario.UserName = query.UserName;

                        usuario.Password = query.Password;

                        usuario.Email = query.Email;

                        usuario.Nombre = query.Nombre;

                        usuario.ApellidoPaterno = query.ApellidoPaterno;

                        usuario.ApellidoMaterno = query.ApellidoMaterno;

                        usuario.Rol = new Rol();

                        usuario.Rol.IdRol = query.IdRol.Value;

                        usuario.Rol.Nombre = query.NombreRol;

                        result.UsuarioObject = usuario; 

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

        public static Usuario GetAll()
        {
            Usuario result = new Usuario();

            try
            {
                using(DL.TrackingAndTraceContext context = new DL.TrackingAndTraceContext())
                {
                    var query = context.Usuarios.FromSqlRaw("UsuarioGetAll").ToList();

                    result.UsuariosObject = new List<Object>();
                    if (query != null)
                    {

                        foreach (var registro in query)
                        {
                            Usuario usuario = new Usuario();

                            usuario.IdUsuario = registro.IdUsuario;

                            usuario.UserName = registro.UserName;

                            usuario.Password = registro.Password;

                            usuario.Email = registro.Email;

                            usuario.Nombre = registro.Nombre;

                            usuario.ApellidoPaterno = registro.ApellidoPaterno;

                            usuario.ApellidoMaterno = registro.ApellidoMaterno;

                            usuario.IdRol = registro.IdRol.Value;

                            usuario.Rol = new Rol();

                            usuario.Rol.Nombre = registro.NombreRol;

                            result.UsuariosObject.Add(usuario);


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

        //Metodo encriptacion de contraseña 
        public static string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));   
                }

                return builder.ToString();

            }
        }

    }


}
