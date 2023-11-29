using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]   
        public IActionResult Usuario()
        {
           BL.Usuario result = BL.Usuario.GetAll(); 

            BL.Usuario usuario = new BL.Usuario();

            usuario.UsuariosObject = result.UsuariosObject;

            return View(usuario);   

        }
        [HttpGet]
        public IActionResult Form(int? IdUsuario)
        {
            BL.Usuario usuario = new BL.Usuario();
            usuario.Rol = new BL.Rol();

            BL.Rol resultRol = BL.Rol.GetAll();

            //Lista de Roles
            usuario.Rol.Roles = resultRol.Roles;

            if(IdUsuario > 0) 
            {
                BL.Usuario result = BL.Usuario.GetById(IdUsuario.Value);
                

                if (result.Correct)
                {
                    usuario = (BL.Usuario)result.UsuarioObject;
                    usuario.Rol.Roles = resultRol.Roles;
                }
            
            }
            else
            {
                usuario.Rol.Roles = resultRol.Roles;
            }

            return View(usuario);   

        }

        [HttpPost]
        public IActionResult Form(BL.Usuario usuario)
        {

            if(usuario.IdUsuario == 0)
            {
                BL.Usuario result = BL.Usuario.Add(usuario);

                if(result.Correct)
                {
                    ViewBag.Mensaje = "Agregado!!!";

                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un Error";
                }

            }
            else
            {
                BL.Usuario result = BL.Usuario.Update(usuario);

                if(result.Correct)
                {
                    ViewBag.Mensaje = "Actualizado!!!";

                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error";
                }
            }

            return PartialView("Modal");   
        }

        public IActionResult Delete(int IdUsuario)
        {
            BL.Usuario result = BL.Usuario.Delete(IdUsuario);

            if (result.Correct)
            {
                ViewBag.Mensaje = "Eliminado!!!";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error!!!";
            }

            return PartialView("Modal");
        }


      


    }
}
