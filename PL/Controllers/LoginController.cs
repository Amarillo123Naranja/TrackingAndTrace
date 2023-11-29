using BL;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            string hashedPassword = BL.Usuario.EncryptPassword(password);
            BL.Usuario usuario = BL.Usuario.GetByIdEmail(email);

            if (usuario.Correct == true)
            {
                usuario = (BL.Usuario)usuario.UsuarioObject;
                string encriptPassword = BL.Usuario.EncryptPassword(usuario.Password);

                if (hashedPassword == encriptPassword)
                    {

                        HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);
                        HttpContext.Session.SetString("Rol", usuario.Rol.Nombre);
                        return RedirectToAction("Index", "Home");   


                     }

                    else

                    {

                        ViewBag.Mensaje = "Contraseña Incorrecta";
                        ViewBag.Login = false;
                        return PartialView("Modal");


                    }

            }
            else
            {
                ViewBag.Mensaje = "No existe la cuenta!!";
                ViewBag.Login = false;
                return PartialView("Modal");
            }

       
        }



    }
}
