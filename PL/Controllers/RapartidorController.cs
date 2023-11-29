using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class RapartidorController : Controller
    {
        //[HttpGet]   
        //public IActionResult GetAll()
        //{
        //    BL.Repartidor result = BL.Repartidor.GetAll();

        //    BL.Repartidor repartidor = new BL.Repartidor(); 

        //    repartidor.Objects = result.Objects; 

        //    return View(repartidor);    
        //}

        //WEB API
        [HttpGet]
        public IActionResult GetAll()
        {
           BL.Repartidor repartidor = new BL.Repartidor();
            repartidor.Objects = new List<Object>();

            //Llamando al servicio
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5289/api/");
                var responseTask = client.GetAsync("Repartidor/GetAll");
                responseTask.Wait();

                var resultServicio = responseTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsAsync<BL.Repartidor>();
                    readTask.Wait();    

                    foreach(var resultRepartidor in readTask.Result.Objects)
                    {
                        BL.Repartidor resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Repartidor>(resultRepartidor.ToString());
                        repartidor.Objects.Add(resultItem);
                    }
                }

            }

            return View(repartidor);


        }

        //[HttpGet]  
        //public IActionResult Form(int? IdRepartidor)
        //{
        //    BL.Repartidor repartidor = new BL.Repartidor();
        //    repartidor.UnidadAsignada = new BL.UnidadAsignada();
        //    BL.UnidadAsignada resultUnidad = BL.UnidadAsignada.GetAll();    

        //    repartidor.UnidadAsignada.Objects = resultUnidad.Objects;   

        //    if(IdRepartidor > 0) 
        //    {
        //        BL.Repartidor result = BL.Repartidor.GetById(IdRepartidor.Value);

        //        if (result.Correct)
        //        {
        //            repartidor = (BL.Repartidor)result.Object;
        //            repartidor.UnidadAsignada.Objects = resultUnidad.Objects;
        //        }


        //    }
        //    else
        //    {
        //        repartidor.UnidadAsignada.Objects = resultUnidad.Objects;
        //    }

        //    return View(repartidor);    

        //}

        //WEB API

        [HttpGet]
        public IActionResult Form(int? IdRepartidor)
        {
            BL.Repartidor repartidor = new BL.Repartidor();
            repartidor.UnidadAsignada = new BL.UnidadAsignada();
            repartidor.Entrega = new BL.Entrega();
            repartidor.UnidadEntrega = new BL.UnidadEntrega();
            repartidor.Usuario = new BL.Usuario();  
            BL.UnidadAsignada resultUnidad = BL.UnidadAsignada.GetAll();
            BL.Entrega entregaResult = BL.Entrega.GetAll(); 
            BL.UnidadEntrega resultEntrega = BL.UnidadEntrega.GetAll(); 
            BL.Usuario resultUsuario = BL.Usuario.GetAll();


            
            if (IdRepartidor > 0)
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5289/api/");
                    var responseTask = client.GetAsync("Repartidor/GetById/" +  IdRepartidor);  
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<BL.Repartidor>(); 
                        readTask.Wait();
                        BL.Repartidor resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Repartidor>(readTask.Result.Object.ToString());
                        repartidor = resultItem;       
                       
                    }
                }

                repartidor.UnidadAsignada.Objects = resultUnidad.Objects;
                repartidor.Entrega.Objects = entregaResult.Objects;
                repartidor.UnidadEntrega.Objects = resultEntrega.Objects;
                repartidor.Usuario.UsuariosObject = resultUsuario.UsuariosObject;


            }
            else
            {
                repartidor.UnidadAsignada.Objects = resultUnidad.Objects;
                repartidor.Entrega.Objects = entregaResult.Objects;
                repartidor.UnidadEntrega.Objects = resultEntrega.Objects;
                repartidor.Usuario.UsuariosObject = resultUsuario.UsuariosObject;
            }

            return View(repartidor);

        }

        //[HttpPost]

        //public IActionResult Form(BL.Repartidor repartidor)
        //{

        //    //if (file.ContentLength > 0)
        //    //{
        //    //    repartidor.Fotografia = ConvertirABase64(file);
        //    //}

        //    if (repartidor.IdRepartidor == 0)
        //    { 


        //        BL.Repartidor result = BL.Repartidor.Add(repartidor);   

        //        if(result.Correct)
        //        {
        //            ViewBag.Mensaje = "Agregado!!";
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "Ocurrio un error";
        //        }
        //    }
        //    else
        //    {
        //        BL.Repartidor result = BL.Repartidor.Update(repartidor);

        //        if(result.Correct)
        //        {
        //            ViewBag.Mensaje = "Actualizado";
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "Ocurrio un Error";
        //        }
        //    }

        //    return PartialView("Modal");  
        //}

        //WEB API
        [HttpPost]

        public IActionResult Form(BL.Repartidor repartidor)
        {

            if (repartidor.IdRepartidor == 0)
            {

                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5289/api/");
                    var postTask = client.PostAsJsonAsync<BL.Repartidor>("Repartidor/Add", repartidor);
                    postTask.Wait();

                    var resultServicio = postTask.Result;


                    if (resultServicio.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Agregado!!";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error";
                    }

                }
               

            }
            else
            {
               using(var client = new HttpClient()) 
                {
                    int IdRepartidor = repartidor.IdRepartidor;

                    client.BaseAddress = new Uri("http://localhost:5289/api/");
                    var putTask = client.PutAsJsonAsync<BL.Repartidor>("Repartidor/" + IdRepartidor, repartidor);
                    putTask.Wait(); 

                    var resultServicio = putTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Actualizado";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un Error";
                    }



                }

               
            }

            return PartialView("Modal");
        }

        //public IActionResult Delete(int IdRepartidor)
        //{
           
        //    BL.Repartidor result = BL.Repartidor.Delete(IdRepartidor);   


        //        if (result.Correct)
        //        {
        //            ViewBag.Mensaje = "Eliminado!!!";
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "Ocurrio un Error!!!";
        //        }


        //    return PartialView("Modal");
        //}


    public IActionResult Delete(int IdRepartidor) 
        { 
           using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5289/api/");
                var deleteTask = client.DeleteAsync("Repartidor/" + IdRepartidor);
                deleteTask.Wait();

                var resultServicio = deleteTask.Result; 

                if (resultServicio.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Eliminado!!!";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un Error!!!";
                }

            }

           
            return PartialView("Modal");
        
        }




        public IActionResult UnidadAsignada(int IdRepartidor)
        {

            BL.Repartidor repartidor = new BL.Repartidor();

            if (IdRepartidor > 0)
            {
                BL.Repartidor result = BL.Repartidor.GetById(IdRepartidor);

                if (result.Correct)
                {
                    repartidor = (BL.Repartidor)result.Object;

                }


            }
            else
            {
                return View();
            }

            return View(repartidor);

        }


        public IActionResult UnidadEntrega(int IdUsuario)
        {
             IdUsuario = (int)HttpContext.Session.GetInt32("IdUsuario");

            BL.Repartidor repartidor = new BL.Repartidor();

            if(IdUsuario >0)
            {
                BL.Repartidor result = BL.Repartidor.GetByIdUnidadEntrega(IdUsuario);

                if (result.Correct)
                {
                    repartidor = (BL.Repartidor) result.Object;
                }


            }
            else
            {
                return View();
            }

            return View(repartidor);  
        }
    }
}
