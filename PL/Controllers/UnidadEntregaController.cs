using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UnidadEntregaController : Controller
    {
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    //string cadena = DL.Conexion.GetConnection();
        //    BL.UnidadEntrega entrega = new BL.UnidadEntrega();
        //    BL.UnidadEntrega result = BL.UnidadEntrega.GetAll();
        //    entrega.Objects = result.Objects;

        //    if (result.Correct)
        //    {
        //        return View(entrega);
        //    }
        //    else
        //    {
        //        return View();

        //    }

        //}

        //WEB API
        [HttpGet]
        public IActionResult GetAll()
        {
            //'http://localhost:5289/api/UnidadEntrega/GetAll'


            BL.UnidadEntrega entrega = new BL.UnidadEntrega();
            entrega.Objects = new List<Object>();

            //Llamando al servicio
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5289/api/");
                var responseTask = client.GetAsync("UnidadEntrega/GetAll");
                responseTask.Wait();

                var resultServicio = responseTask.Result;   

                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsAsync<BL.UnidadEntrega>();
                    readTask.Wait();

                    foreach (var resultUnidad in readTask.Result.Objects)
                    {
                        BL.UnidadEntrega resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.UnidadEntrega>(resultUnidad.ToString());
                        entrega.Objects.Add(resultItem);

                    }
                    
                }
               

            }

            return View(entrega);     

        }

        //[HttpGet]
        //public IActionResult Form(int? IdUnidad)
        //{
        //    BL.UnidadEntrega entrega = new BL.UnidadEntrega();
        //    entrega.EstatusUnidad = new BL.EstatusUnidad();

        //    BL.EstatusUnidad resultUnidad = BL.EstatusUnidad.GetAll();  

        //    if(IdUnidad != null)
        //    {
        //        BL.UnidadEntrega result = BL.UnidadEntrega.GetById(IdUnidad.Value);

        //        if (result.Correct)
        //        {
        //            entrega = (BL.UnidadEntrega)result.Object;
        //            entrega.EstatusUnidad.Objects = resultUnidad.Objects;     
        //        }
        //    }
        //    else
        //    {
        //        entrega.EstatusUnidad.Objects = resultUnidad.Objects;
        //    }

        //    return View(entrega);   

        //}

        //WEB API
        [HttpGet]
        public IActionResult Form(int? IdUnidad)
        {
            BL.UnidadEntrega entrega = new BL.UnidadEntrega();
            entrega.EstatusUnidad = new BL.EstatusUnidad();
            BL.EstatusUnidad resultUnidad = BL.EstatusUnidad.GetAll();

            if (IdUnidad != null)
            {
               using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5289/api/");
                    var resposeTask = client.GetAsync("UnidadEntrega/GetById/" + IdUnidad);
                    resposeTask.Wait();

                    var resultServicio = resposeTask.Result;    

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<BL.UnidadEntrega>();
                        readTask.Wait();
                        BL.UnidadEntrega resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.UnidadEntrega>(readTask.Result.Object.ToString());
                        entrega = resultItem;

                        
                    }
                }

                entrega.EstatusUnidad.Objects = resultUnidad.Objects;


            }
            else
            {
                entrega.EstatusUnidad.Objects = resultUnidad.Objects;
            }

            return View(entrega);

        }

        //[HttpPost]  

        //public IActionResult Form (BL.UnidadEntrega entrega)
        //{
        //    if(entrega.IdUnidad == 0)
        //    {
        //        BL.UnidadEntrega result = BL.UnidadEntrega.Add(entrega);    

        //        if(result.Correct)
        //        {
        //            ViewBag.Mensaje = "Agregado!!!";
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "Ocurrio un error";
        //        }

                

        //    }
        //    else
        //    {
        //        BL.UnidadEntrega result = BL.UnidadEntrega.Update(entrega); 

        //        if(result.Correct) 
        //        {

        //            ViewBag.Mensaje = "Actualizado";
                
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "Ocurrio un error";
        //        }

                
        //    }

        //    return PartialView("Modal");
        //}

        //WEB API
        [HttpPost]
        public IActionResult Form(BL.UnidadEntrega entrega)
        {
            if (entrega.IdUnidad == 0)
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5289/api/");
                    var postTask = client.PostAsJsonAsync<BL.UnidadEntrega>("UnidadEntrega/Add", entrega);
                    postTask.Wait();

                    var resultServicio = postTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Agregado!!!";
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
                    int IdUnidad = entrega.IdUnidad;

                    client.BaseAddress = new Uri("http://localhost:5289/api/");
                    var putTask = client.PutAsJsonAsync<BL.UnidadEntrega>("UnidadEntrega/" + IdUnidad, entrega);
                    putTask.Wait();

                    var resultServicio = putTask.Result;    

                    if (resultServicio.IsSuccessStatusCode)
                    {

                        ViewBag.Mensaje = "Actualizado";

                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error";
                    }




                }



            }

            return PartialView("Modal");
        }


        public IActionResult Delete(int IdUnidad)
        {
            using(var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("http://localhost:5289/api/");
                var deleteTask = client.DeleteAsync("UnidadEntrega/" + IdUnidad);
                deleteTask.Wait();  

                var resultServicio = deleteTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Eliminado!!";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error";
                }


            }

           

            return PartialView("Modal");
        }


    }
}
