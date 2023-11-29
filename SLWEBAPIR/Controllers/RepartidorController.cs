using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SLWEBAPIR.Controllers
{
    [Route("api/Repartidor")]
    [ApiController]
    public class RepartidorController : ControllerBase
    {
        [Route("Add")]
        [HttpPost]   

        public IActionResult Add(BL.Repartidor repartidor)
        {
            BL.Repartidor result = BL.Repartidor.Add(repartidor);

            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [Route("{IdRepartidor}")]
        [HttpPut]
        
        public IActionResult Update(int IdRepartidor, [FromBody] BL.Repartidor repartidor) 
        {
            repartidor.IdRepartidor = IdRepartidor;

            BL.Repartidor result = BL.Repartidor.Update(repartidor);    

            if(result.Correct) 
            {
                return StatusCode(200, result); 
            
            }
            else
            {
                return StatusCode(400, result);
            }
        
        }

        [Route("{IdRepartidor}")]
        [HttpDelete]   
        public IActionResult Delete(int IdRepartidor) 
        {
            BL.Repartidor result = BL.Repartidor.Delete(IdRepartidor);

            if(result.Correct)
            {
               return StatusCode(200, result);    
            }
            else
            {
                return StatusCode(400, result);
            }
        
        
        }

        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            BL.Repartidor result = BL.Repartidor.GetAll();

            if (result.Correct)
            {
                return StatusCode(200, result); 
            }
            else
            {
                return StatusCode(200, result);
            }

        }

        //http://localhost:5289/api/Repartidor/GetById?IdRepartidor=1

        [Route("GetById/{IdRepartidor}")]
        [HttpGet]   

        public IActionResult GetById(int IdRepartidor)
        {
            BL.Repartidor result = BL.Repartidor.GetById(IdRepartidor);

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
