using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SLWEBAPIR.Controllers
{
    [Route("api/UnidadEntrega")]
    [ApiController]
    public class UnidadController : ControllerBase
    {
        [Route("Add")]
        [HttpPost]
        public IActionResult Add(BL.UnidadEntrega unidad)
        {
            BL.UnidadEntrega result = BL.UnidadEntrega.Add(unidad);

            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return StatusCode(400, result);
            }


        }

        [Route("{IdUnidad}")]
        [HttpPut]
        
        public IActionResult Update(int IdUnidad, [FromBody] BL.UnidadEntrega unidad) 
        {
            unidad.IdUnidad = IdUnidad; 
            BL.UnidadEntrega result = BL.UnidadEntrega.Update(unidad);  

            if (result.Correct) 
            {
                return StatusCode(200, result);
            
            }
            else
            {
                return StatusCode(400, result);
            }
        
        
        }
        [Route("{IdUnidad}")]
        [HttpDelete]
        public IActionResult Delete(int IdUnidad) 
        {
            BL.UnidadEntrega result = BL.UnidadEntrega.Delete(IdUnidad);

            if (result.Correct)
            {

                return StatusCode(200, result);
            }
            else
            {
                return StatusCode(400,result);  
            }
        }


        [Route("GetAll")]
        [HttpGet]   
        public IActionResult GetAll()
        {
            BL.UnidadEntrega result = BL.UnidadEntrega.GetAll();

            if (result.Correct)
            {
                return StatusCode(200, result); 
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [Route("GetById/{IdUnidad}")]
        [HttpGet]   
        public IActionResult GetById(int IdUnidad)
        {
            BL.UnidadEntrega result = BL.UnidadEntrega.GetById(IdUnidad);

            if (result.Correct)
            {
                return StatusCode(200, result); 
            }
            else
            {
                return StatusCode(400, result);
            }
        }    
    }
}
