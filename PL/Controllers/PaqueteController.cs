using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.Kernel.Pdf;

namespace PL.Controllers
{
    public class PaqueteController : Controller
    {
        [HttpGet]
        public IActionResult CodigoRastreo(string codigoRastreo)
        {
            BL.Paquete paquete = BL.Paquete.GetByIdCodigoRastreo(codigoRastreo);

            if (paquete.Correct)
            {
                paquete = (BL.Paquete)paquete.Object;

                if (paquete.CodigoRastreo == codigoRastreo)
                {
                    return View(paquete);
                }
                else
                {
                    ViewBag.Mensaje = "Codigo de rastreo Erroneo";
                    return RedirectToAction("Index", "Home");

                }

            }
            else
            {
                ViewBag.Mensaje = "No existe codigo de rastreo";
                return RedirectToAction("Index", "Home");
            }
              


        }


        [HttpGet]
        public IActionResult Paquete()
        {
           
            BL.Paquete paquete = new BL.Paquete();

            paquete.Entrega = new BL.Entrega();

            paquete.Entrega.EstatusEntrega = new BL.EstatusEntrega();

            paquete.Entrega.Repartidor = new BL.Repartidor();   

            paquete.Entrega.Repartidor.Usuario = new BL.Usuario();

            paquete.Entrega.Repartidor.Usuario.Nombre = "";

            paquete.Entrega.EstatusEntrega.IdEstatusEntrega = 0;

            BL.Paquete result = BL.Paquete.PaqueteGetAll(paquete.Entrega.Repartidor.Usuario.Nombre, paquete.Entrega.EstatusEntrega.IdEstatusEntrega);

            BL.EstatusEntrega entrega = BL.EstatusEntrega.GetAll();

            paquete.Entrega.EstatusEntrega.Objects = entrega.Objects;

            paquete.Objects = result.Objects;


            return View(paquete);  
        }
        
        [HttpPost]  
        public IActionResult Paquete(string nombre, int idEstatusEntrega)
        {

            BL.Paquete paquete = new BL.Paquete();

            paquete.Entrega = new BL.Entrega();

            paquete.Entrega.EstatusEntrega = new BL.EstatusEntrega();

            paquete.Entrega.Repartidor = new BL.Repartidor();

            paquete.Entrega.Repartidor.Usuario = new BL.Usuario();

            if (paquete.Entrega.Repartidor.Usuario.Nombre == null)
            {
                paquete.Entrega.Repartidor.Usuario.Nombre = "";
            }
            if (paquete.Entrega.EstatusEntrega.IdEstatusEntrega == 0)
            {
                paquete.Entrega.EstatusEntrega.IdEstatusEntrega = 0;

                idEstatusEntrega = 1;
            }

            BL.EstatusEntrega entrega = BL.EstatusEntrega.GetAll();
            paquete.Entrega.EstatusEntrega.Objects = entrega.Objects;

            BL.Paquete result = BL.Paquete.PaqueteGetAll(nombre, idEstatusEntrega);

            paquete.Objects = result.Objects;

            return View(paquete);

        }



        [HttpGet]//Muestra el formulario
        public IActionResult CrearPaquete(int? IdPaquete)
        {
            BL.Paquete paquete = new BL.Paquete();
            paquete.Entrega = new BL.Entrega();
            BL.Entrega resultEntrega = BL.Entrega.GetAll();

            if(IdPaquete == 0)
            {
                BL.Paquete result = BL.Paquete.GetById(IdPaquete.Value);

                if (result.Correct)
                {
                    paquete = ( BL.Paquete)result.Object;  
                    paquete.Entrega.Objects = resultEntrega.Objects;   

                }
            }
            else
            {
                paquete.Entrega.Objects = resultEntrega.Objects;
            }

            return View(paquete);

        }

        [HttpPost]//Manda los datos del formulario
        public IActionResult CrearPaquete(BL.Paquete paquete, string email)
        {
            string emailOrigen = "jazminnellysc@gmail.com";
           
            if(paquete.IdPaquete == 0)
            {
                BL.Paquete result = BL.Paquete.Add(paquete);

                if (result.Correct)
                {

                    MailMessage mailMessage = new MailMessage(emailOrigen, email, "Paquete", "<p>Detalle de su paquete</p>" + "<br/>" + paquete.Detalle + "<br/>" + paquete.DireccionOrigen + "<br/>" + paquete.DireccionEntrega);

                    mailMessage.IsBodyHtml = true;

                    string url = "http://192.168.0.104/Paquete/DetallePaquete/ " + System.Web.HttpUtility.UrlEncode(email);

                    mailMessage.Body = mailMessage.Body.Replace("{Url}", url);
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, "eubeshajyzgkkaaj");

                    smtpClient.Send(mailMessage);
                    smtpClient.Dispose();

                    ViewBag.Mensaje = "Se ha enviado un correo de confirmación a tu correo electronico";
                }
                else
                {
                    ViewBag.Mensaje = "Error al crear Paquete!!!";
                }
                
            }
            else
            {
                //Actualizar
            }

            return PartialView("Modal");

        }


        public IActionResult GenerarPdf()
        {
            BL.Paquete paquete = new BL.Paquete();

            paquete.Entrega = new BL.Entrega();

            paquete.Entrega.EstatusEntrega = new BL.EstatusEntrega();

            paquete.Entrega.Repartidor = new BL.Repartidor();

            paquete.Entrega.Repartidor.Usuario = new BL.Usuario();

            paquete.Entrega.Repartidor.Usuario.Nombre = "";

            paquete.Entrega.EstatusEntrega.IdEstatusEntrega = 0;

            BL.Paquete result = BL.Paquete.PaqueteGetAll(paquete.Entrega.Repartidor.Usuario.Nombre, paquete.Entrega.EstatusEntrega.IdEstatusEntrega);

            BL.EstatusEntrega entrega = BL.EstatusEntrega.GetAll();

            paquete.Entrega.EstatusEntrega.Objects = entrega.Objects;

            paquete.Objects = result.Objects;
            // paquete.Objects = new List<Object>();
            Paquete("Johan", 1);

            //Crear un nuevo documento PDF en una ubicacion temporal
            string rutaTempPDF = Path.GetTempFileName() + ".pdf";

            using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(rutaTempPDF)))
            {
                using (Document document = new Document(pdfDocument))
                {
                    document.Add(new Paragraph("Detalle Paquetes"));
                    //Crear la tabla para mostrar la lista de Objetos
                    Table table = new Table(7); // 5 columnas
                    table.SetWidth(UnitValue.CreatePercentValue(100));//Ancho de la tabla al 100% del documento

                    //Añadir las celdas de encabezado a la tabla
                    
                
                    table.AddHeaderCell("Detalle");
                    table.AddHeaderCell("Direccion Origen");
                    table.AddHeaderCell("Direccion Entrega");
                    table.AddHeaderCell("Codigo de rastreo");
                    table.AddHeaderCell("Estatus");
                    table.AddHeaderCell("Fotografia");
                    table.AddHeaderCell("Nombre");
                   

                    foreach (BL.Paquete pa in paquete.Objects)
                    {
                       
                        table.AddCell(pa.Detalle);
                        table.AddCell(pa.DireccionOrigen);
                        table.AddCell(pa.DireccionEntrega);
                        table.AddCell(pa.CodigoRastreo);
                        table.AddCell(pa.Entrega.EstatusEntrega.Estatus);
                        byte[] imageBytes = Convert.FromBase64String(pa.Entrega.Repartidor.Fotografia);
                        ImageData data = ImageDataFactory.Create(imageBytes);
                        Image image = new Image(data);
                        table.AddCell(image.SetWidth(50).SetHeight(50));
                        table.AddCell(pa.Entrega.Repartidor.Usuario.Nombre);

                    }

                    //Añadir la tabla al documento
                    document.Add(table);


                }
            }


            // Leer el archivo PDF como un arreglo de bytes
            byte[] fileBytes = System.IO.File.ReadAllBytes(rutaTempPDF);

            // Eliminar el archivo temporal
            System.IO.File.Delete(rutaTempPDF);
           /* HttpContext.Session.Remove("Carrito")*/;

            // Descargar el archivo PDF
            return new FileStreamResult(new MemoryStream(fileBytes), "application/pdf")
            {
                FileDownloadName = "Detalle Paquetes.pdf"
            };
        }



    }



}

